using System.Reflection.Emit;

namespace EverythingAlways.Patches
{
    //[HarmonyPatch(typeof(StartPracticeMode), "OnUpdate")]
    static class StartPracticeMode_Patch
    {
        static readonly List<OpCode> OPCODES_TO_MATCH = new List<OpCode>()
        {
            OpCodes.Nop,
            OpCodes.Ldarg,
            OpCodes.Ldflda,
            OpCodes.Call,
            OpCodes.Stloc,
            OpCodes.Ldloc,
            OpCodes.Ldfld,
            OpCodes.Call,
            OpCodes.Brtrue
        };

        static readonly List<object> OPERANDS_TO_MATCH = new List<object>() { };

        static readonly List<OpCode> MODIFIED_OPCODES = new List<OpCode>() { };

        static readonly List<object> MODIFIED_OPERANDS = new List<object>()
        {
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            typeof(IngredientLib.Main).GetMethod("StartPractice", BindingFlags.Public | BindingFlags.Static)
        };

        const int EXPECTED_MATCH_COUNT = 1;

        //[HarmonyTranspiler]
        static IEnumerable<CodeInstruction> OriginalLambdaBody_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            IngredientLib.Main.LogInfo("StartPracticeMode Transpiler");
            IngredientLib.Main.LogInfo("Attempt to disable practice mode on a corrupted save");
            List<CodeInstruction> list = instructions.ToList();

            int matches = 0;
            int windowSize = OPCODES_TO_MATCH.Count;
            for (int i = 0; i < list.Count - windowSize; i++)
            {
                for (int j = 0; j < windowSize; j++)
                {
                    if (OPCODES_TO_MATCH[j] == null)
                    {
                        return instructions;
                    }

                    int index = i + j;
                    OpCode opCode = list[index].opcode;
                    if (j < OPCODES_TO_MATCH.Count && opCode != OPCODES_TO_MATCH[j])
                    {
                        break;
                    }

                    if (j < OPERANDS_TO_MATCH.Count && OPERANDS_TO_MATCH[j] != null)
                    {
                        object operand = list[index].operand;
                        if (OPERANDS_TO_MATCH[j] != operand)
                        {
                            break;
                        }
                    }

                    if (j == OPCODES_TO_MATCH.Count - 1)
                    {
                        if (matches > EXPECTED_MATCH_COUNT)
                        {
                            return instructions;
                        }

                        // Perform replacements
                        for (int k = 0; k < MODIFIED_OPCODES.Count; k++)
                        {
                            if (MODIFIED_OPCODES[k] != null)
                            {
                                int replacementIndex = i + k;
                                OpCode beforeChange = list[replacementIndex].opcode;
                                list[replacementIndex].opcode = MODIFIED_OPCODES[k];
                                IngredientLib.Main.LogInfo($"Line {replacementIndex}: Replaced Opcode ({beforeChange} ==> {MODIFIED_OPCODES[k]})");
                            }
                        }

                        for (int k = 0; k < MODIFIED_OPERANDS.Count; k++)
                        {
                            if (MODIFIED_OPERANDS[k] != null)
                            {
                                int replacementIndex = i + k;
                                object beforeChange = list[replacementIndex].operand;
                                list[replacementIndex].operand = MODIFIED_OPERANDS[k];
                                IngredientLib.Main.LogInfo($"Line {replacementIndex}: Replaced operand ({beforeChange ?? "null"} ==> {MODIFIED_OPERANDS[k] ?? "null"})");
                            }
                        }
                    }
                }
            }

            return list.AsEnumerable();
        }
    }
}
