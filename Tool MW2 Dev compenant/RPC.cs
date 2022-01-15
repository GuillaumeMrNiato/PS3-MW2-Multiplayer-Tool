using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PS3Lib;


namespace MW2_TEST
{
    class RPC
    {
        private static PS3API PS3 = new PS3API();

        private static uint function_address = 0x38EDE8;
        public static int Call(uint func_address, params object[] parameters)
        {
            int length = parameters.Length;
            uint num2 = 0;
            for (uint i = 0; i < length; i++)
            {
                if (parameters[i] is int)
                {
                    byte[] array = BitConverter.GetBytes((int)parameters[i]);
                    Array.Reverse(array);
                    PS3.SetMemory(0x10050000 + ((i + num2) * 4), array);
                }
                else if (parameters[i] is uint)
                {
                    byte[] buffer2 = BitConverter.GetBytes((uint)parameters[i]);
                    Array.Reverse(buffer2);
                    PS3.SetMemory(0x10050000 + ((i + num2) * 4), buffer2);
                }
                else if (parameters[i] is string)
                {
                    byte[] buffer3 = Encoding.UTF8.GetBytes(Convert.ToString(parameters[i]) + "\0");
                    PS3.SetMemory(0x10050054 + (i * 0x400), buffer3);
                    uint num4 = 0x10050054 + (i * 0x400);
                    byte[] buffer4 = BitConverter.GetBytes(num4);
                    Array.Reverse(buffer4);
                    PS3.SetMemory(0x10050000 + ((i + num2) * 4), buffer4);
                }
                else if (parameters[i] is float)
                {
                    num2++;
                    byte[] buffer5 = BitConverter.GetBytes((float)parameters[i]);
                    Array.Reverse(buffer5);
                    PS3.SetMemory(0x10050024 + ((num2 - 1) * 4), buffer5);
                }
            }
            byte[] bytes = BitConverter.GetBytes(func_address);
            Array.Reverse(bytes);
            PS3.SetMemory(0x1005004c, bytes);
            System.Threading.Thread.Sleep(20);
            byte[] memory = new byte[4];
            PS3.GetMemory(0x10050050, memory);
            Array.Reverse(memory);
            return BitConverter.ToInt32(memory, 0);
        }

        public static void EnableRPC()
        {
            byte[] memory = new byte[] { 0xF8, 0x21, 0xFF, 0x91, 0x7C, 0x08, 0x02, 0xA6, 0xF8, 0x01, 0x00, 0x80, 0x3C, 0x40, 0x00, 0x72, 0x30, 0x42, 0x4C, 0x38, 0x3C, 0x60, 0x10, 0x05, 0x81, 0x83, 0x00, 0x4C, 0x2C, 0x0C, 0x00, 0x00, 0x41, 0x82, 0x00, 0x64, 0x80, 0x83, 0x00, 0x04, 0x80, 0xA3, 0x00, 0x08, 0x80, 0xC3, 0x00, 0x0C, 0x80, 0xE3, 0x00, 0x10, 0x81, 0x03, 0x00, 0x14, 0x81, 0x23, 0x00, 0x18, 0x81, 0x43, 0x00, 0x1C, 0x81, 0x63, 0x00, 0x20, 0xC0, 0x23, 0x00, 0x24, 0xC0, 0x43, 0x00, 0x28, 0xC0, 0x63, 0x00, 0x2C, 0xC0, 0x83, 0x00, 0x30, 0xC0, 0xA3, 0x00, 0x34, 0xC0, 0xC3, 0x00, 0x38, 0xC0, 0xE3, 0x00, 0x3C, 0xC1, 0x03, 0x00, 0x40, 0xC1, 0x23, 0x00, 0x48, 0x80, 0x63, 0x00, 0x00, 0x7D, 0x89, 0x03, 0xA6, 0x4E, 0x80, 0x04, 0x21, 0x3C, 0x80, 0x10, 0x05, 0x38, 0xA0, 0x00, 0x00, 0x90, 0xA4, 0x00, 0x4C, 0x80, 0x64, 0x00, 0x50, 0x3C, 0x40, 0x00, 0x73, 0x30, 0x42, 0x4B, 0xE8, 0xE8, 0x01, 0x00, 0x80, 0x7C, 0x08, 0x03, 0xA6, 0x38, 0x21, 0x00, 0x70, 0x4E, 0x80, 0x00, 0x20 };
            PS3.SetMemory(function_address, memory);
            PS3.SetMemory(0x10050000, new byte[0x2854]);
        }
    }
}
