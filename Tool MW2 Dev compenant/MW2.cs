using System;
using System.Threading;
using PS3Lib;

namespace MW2Lib
{
    public class MW2
    {
        private static PS3API DEX = new PS3API();

        public class HudStruct
        {
            public static uint
            xOffset = 0x08,
            yOffset = 0x04,
            textOffset = 0x84,
            GlowColor = 0x8C,
            fontOffset = 0x28,
            fontSizeOffset = 0x14,
            colorOffset = 0x34,
            relativeOffset = 0x2c,
            widthOffset = 0x48,
            heightOffset = 0x44,
            shaderOffset = 0x4C,
            alignOffset = 0x30,
            flags = 0xA4,
            clientIndex = 0xA8;
        }

        public static void WritePowerPc(bool Active)
        {
            byte[] NewPPC = new byte[] { 0xF8, 0x21, 0xFF, 0x61, 0x7C, 0x08, 0x02, 0xA6, 0xF8, 0x01, 0x00, 0xB0, 0x3C, 0x60, 0x10, 0x03, 0x80, 0x63, 0x00, 0x00, 0x60, 0x62, 0x00, 0x00, 0x3C, 0x60, 0x10, 0x04, 0x80, 0x63, 0x00, 0x00, 0x2C, 0x03, 0x00, 0x00, 0x41, 0x82, 0x00, 0x28, 0x3C, 0x60, 0x10, 0x04, 0x80, 0x63, 0x00, 0x04, 0x3C, 0xA0, 0x10, 0x04, 0x38, 0x80, 0x00, 0x00, 0x30, 0xA5, 0x00, 0x10, 0x4B, 0xE8, 0xB2, 0x7D, 0x38, 0x60, 0x00, 0x00, 0x3C, 0x80, 0x10, 0x04, 0x90, 0x64, 0x00, 0x00, 0x3C, 0x60, 0x10, 0x05, 0x80, 0x63, 0x00, 0x00, 0x2C, 0x03, 0x00, 0x00, 0x41, 0x82, 0x00, 0x24, 0x3C, 0x60, 0x10, 0x05, 0x30, 0x63, 0x00, 0x10, 0x4B, 0xE2, 0xF9, 0x7D, 0x3C, 0x80, 0x10, 0x05, 0x90, 0x64, 0x00, 0x04, 0x38, 0x60, 0x00, 0x00, 0x3C, 0x80, 0x10, 0x05, 0x90, 0x64, 0x00, 0x00, 0x3C, 0x60, 0x10, 0x03, 0x80, 0x63, 0x00, 0x04, 0x60, 0x62, 0x00, 0x00, 0xE8, 0x01, 0x00, 0xB0, 0x7C, 0x08, 0x03, 0xA6, 0x38, 0x21, 0x00, 0xA0, 0x4E, 0x80, 0x00, 0x20 };
            byte[] RestorePPC = new byte[] { 0x81, 0x62, 0x92, 0x84, 0x7C, 0x08, 0x02, 0xA6, 0xF8, 0x21, 0xFF, 0x01, 0xFB, 0xE1, 0x00, 0xB8, 0xDB, 0x01, 0x00, 0xC0, 0x7C, 0x7F, 0x1B, 0x78, 0xDB, 0x21, 0x00, 0xC8, 0xDB, 0x41, 0x00, 0xD0, 0xDB, 0x61, 0x00, 0xD8, 0xDB, 0x81, 0x00, 0xE0, 0xDB, 0xA1, 0x00, 0xE8, 0xDB, 0xC1, 0x00, 0xF0, 0xDB, 0xE1, 0x00, 0xF8, 0xFB, 0x61, 0x00, 0x98, 0xFB, 0x81, 0x00, 0xA0, 0xFB, 0xA1, 0x00, 0xA8, 0xFB, 0xC1, 0x00, 0xB0, 0xF8, 0x01, 0x01, 0x10, 0x81, 0x2B, 0x00, 0x00, 0x88, 0x09, 0x00, 0x0C, 0x2F, 0x80, 0x00, 0x00, 0x40, 0x9E, 0x00, 0x64, 0x7C, 0x69, 0x1B, 0x78, 0xC0, 0x02, 0x92, 0x94, 0xC1, 0xA2, 0x92, 0x88, 0xD4, 0x09, 0x02, 0x40, 0xD0, 0x09, 0x00, 0x0C, 0xD1, 0xA9, 0x00, 0x04, 0xD0, 0x09, 0x00, 0x08, 0xE8, 0x01, 0x01, 0x10, 0xEB, 0x61, 0x00, 0x98, 0xEB, 0x81, 0x00, 0xA0, 0x7C, 0x08, 0x03, 0xA6, 0xEB, 0xA1, 0x00, 0xA8, 0xEB, 0xC1, 0x00, 0xB0, 0xEB, 0xE1, 0x00, 0xB8, 0xCB, 0x01, 0x00, 0xC0, 0xCB, 0x21, 0x00, 0xC8 };
            if (Active == true)
                DEX.SetMemory(0x0038EDE8, NewPPC);
            else
                DEX.SetMemory(0x0038EDE8, RestorePPC);
        }

        public static void SV_SendServerCommand(int clientIndex, string Command)
        {
            MW2.WritePowerPc(true);
            DEX.Extension.WriteString(0x10040010, Command);
            DEX.Extension.WriteInt32(0x10040004, clientIndex);
            DEX.Extension.WriteBool(0x10040003, true);
            bool isRunning;
            do { isRunning = DEX.Extension.ReadBool(0x10040003); } while (isRunning != false);
            MW2.WritePowerPc(false);
        }

        public static class HudAlloc
        {
            public static uint
                IndexSlot = 50,
                g_hudelem = 0x012E9858;

            public static bool
                Start = true;
        }

        public static class HUDAlign
        {
            public static uint
                RIGHT = 2,
                CENTER = 5,
                LEFT = 1;
        }

        public class HudTypes
        {
            public static uint
                Text = 1,
                Shader = 6,
                Null = 0;
        }

        public class Material
        {
            public static uint
                White = 1,
                Black = 2,
                Prestige0 = 0x1A,
                Prestige1 = 0x1B,
                Prestige2 = 0x1C,
                Prestige3 = 0x1D,
                Prestige4 = 0x1E,
                Prestige5 = 0x1F,
                Prestige6 = 0x20,
                Prestige7 = 0x21,
                Prestige8 = 0x22,
                Prestige9 = 0x23,
                Prestige10 = 0x24,
                WhiteRectangle = 0x25,
                NoMap = 0x29;
        }

        public static uint HudElemAlloc(bool Reset = false)
        {
            if (Reset == true)
                HudAlloc.IndexSlot = 50;
            uint Output = HudAlloc.g_hudelem + (HudAlloc.IndexSlot * 0xB4);
            HudAlloc.IndexSlot++;
            return Output;
        }

        public static uint G_LocalizedString(string input)
        {
            uint StrIndex = 0;
            bool isRunning = true;
            MW2.WritePowerPc(true);
            DEX.Extension.WriteString(0x10050010, input);
            DEX.Extension.WriteBool(0x10050000 + 3, true);
            do { StrIndex = DEX.Extension.ReadUInt32(0x10050004); } while (StrIndex == 0);
            DEX.Extension.WriteUInt32(0x10050004, 0);
            do { isRunning = DEX.Extension.ReadBool(0x10050003); } while (isRunning != false);
            MW2.WritePowerPc(false);
            return StrIndex;
        }

        public static int RGB2INT(int r, int g, int b, int a)
        {
            byte[] newRGB = new byte[4];
            newRGB[0] = (byte)r;
            newRGB[1] = (byte)g;
            newRGB[2] = (byte)b;
            newRGB[3] = (byte)a;
            Array.Reverse(newRGB);
            return BitConverter.ToInt32(newRGB, 0);
        }

        public static void SetText(uint clientIndex, uint elem, uint text, uint font, float fontScale, float x, float y, uint alignText, uint align, int r = 255, int g = 255, int b = 255, int a = 255, int GlowR = 255, int GlowG = 0, int GlowB = 0, int GlowA = 0)
        {
            DEX.Extension.WriteInt32(elem, 0);
            DEX.Extension.WriteInt32(elem + HudStruct.flags, 1);
            DEX.Extension.WriteUInt32(elem + HudStruct.clientIndex, clientIndex);
            DEX.Extension.WriteUInt32(elem + HudStruct.textOffset, text);
            DEX.Extension.WriteUInt32(elem + HudStruct.relativeOffset, alignText);
            DEX.Extension.WriteUInt32(elem + HudStruct.relativeOffset - 4, 6);
            DEX.Extension.WriteUInt32(elem + HudStruct.fontOffset, font);
            DEX.Extension.WriteUInt32(elem + HudStruct.alignOffset, align);
            DEX.Extension.WriteInt16(elem + HudStruct.textOffset + 4, 0x4000);
            DEX.Extension.WriteFloat(elem + HudStruct.fontSizeOffset, fontScale);
            DEX.Extension.WriteFloat(elem + HudStruct.xOffset, x);
            DEX.Extension.WriteFloat(elem + HudStruct.yOffset, y);
            DEX.Extension.WriteInt32(elem + HudStruct.colorOffset, RGB2INT(r, g, b, a));
            DEX.Extension.WriteInt32(elem + HudStruct.GlowColor, RGB2INT(GlowR, GlowG, GlowB, GlowA));
        }

        public static void SetShader(uint clientIndex, uint elem, uint shader, int width, int height, float x, float y, uint align, float sort = 0, int r = 255, int g = 255, int b = 255, int a = 255)
        {
            DEX.Extension.WriteInt32(elem, 0);
            DEX.Extension.WriteInt32(elem + HudStruct.flags, 1);
            DEX.Extension.WriteUInt32(elem + HudStruct.clientIndex, clientIndex);
            DEX.Extension.WriteUInt32(elem + HudStruct.shaderOffset, shader);
            DEX.Extension.WriteUInt32(elem + HudStruct.relativeOffset, 5);
            DEX.Extension.WriteUInt32(elem + HudStruct.relativeOffset - 4, 6);
            DEX.Extension.WriteInt32(elem + HudStruct.heightOffset, height);
            DEX.Extension.WriteInt32(elem + HudStruct.widthOffset, width);
            DEX.Extension.WriteUInt32(elem + HudStruct.alignOffset, align);
            DEX.Extension.WriteFloat(elem + HudStruct.xOffset, x);
            DEX.Extension.WriteFloat(elem + HudStruct.yOffset, y);
            DEX.Extension.WriteInt32(elem + HudStruct.colorOffset, RGB2INT(r, g, b, a));
            DEX.Extension.WriteFloat(elem + HudStruct.textOffset + 4, sort);
        }

        public static void SetElement(uint Element, uint HudTypes)
        {
            DEX.Extension.WriteUInt32(Element, HudTypes);
        }

        public static void SetClientDvars(int client, string dvars)
        {
            SV_SendServerCommand(client, "v " + dvars);
        }

        public static void iPrintln(int client, string message)
        {
            SV_SendServerCommand(client, "f \"" + message);
        }

        public static void iPrintlnBold(int client, string message)
        {
            SV_SendServerCommand(client, "c \"" + message);
        }

        public static void GiveUnlockAll(int client)
        {
            MW2.SV_SendServerCommand(client, "v loc_warnings \"0\"");
            MW2.SV_SendServerCommand(client, "v loc_warningsAsErrors \"0\"");
            MW2.SV_SendServerCommand(client, "N 2056 206426 6525 7F 3760 09 4623 E803 3761 09 4627 F430 3762 02 4631 14 3763 02 4635 3C 3764 02 4639 0F 3765 02 4643 14 3766 02 4647 28 3767 02 4651 0A 3752 09 4591 E803 3753 09 4595 0F40 3754 02 4599 14 3755 02 4603 3C 3756 02 4607 0F 3757 02 4611 14 3758 02 4615 28 3759 02 4619 0A 3736 09 4527 E803");
            MW2.SV_SendServerCommand(client, "N 3737 09 4531 0F40 3738 02 4535 14 3739 02 4539 3C 3740 02 4543 0F 3741 02 4547 14 3742 02 4551 28 3743 02 4555 0A 3799 09 4779 E803 3800 09 4783 0F40 3801 02 4787 14 3802 02 4791 3C 3803 02 4795 0F 3804 02 4799 14 3805 02 4803 28 3806 02 4807 0A");
            MW2.SV_SendServerCommand(client, "N 3775 09 4683 E803 3776 09 4687 0F40 3777 02 4691 14 3778 02 4695 3C 3779 02 4699 0F 3780 02 4703 14 3781 02 4707 28 3782 02 4711 0A 3728 09 4495 E803 3729 09 4499 0F40 3730 02 4503 14 3731 02 4507 3C 3732 02 4511 0F 3733 02 4515 14 3734 02 4519 28 3735 02 4523 0A 3783 09 4715 E803 3784 09 4719 0F40 3785 02 4723 14 3786 02 4727 3C");
            MW2.SV_SendServerCommand(client, "N 3787 02 4731 0F 3788 02 4735 14 3789 02 4739 28 3790 02 4743 0A 3791 09 4747 E803 3864 02 5039 14 3865 02 5043 28 3866 02 5047 09 3888 09 5135 E803 3887 09 5131 0F40");
            MW2.SV_SendServerCommand(client, "N 3792 09 4751 0F40 3793 02 4755 14 3794 02 4759 3C 3795 02 4763 0F 3796 02 4767 14 3797 02 4771 28 3798 02 4775 0A 3744 09 4559 E803 3745 09 4563 0F40 3746 02 4567 14 3889 02 5139 0F 3890 02 5143 3C 3891 02 5147 14 3892 02 5151 28 3893 02 5155 09 3807 09 4811 E803 3808 09 4815 0F40 3809 02 4819 0F 3810 02 4823 14 3811 02 4827 28");
            MW2.SV_SendServerCommand(client, "N 3747 02 4571 3C 3748 02 4575 0F 3749 02 4579 14 3750 02 4583 28 3751 02 4587 0A 3853 09 4995 E803 3854 09 4999 0F40 3855 02 5003 1E 3856 02 5007 3C 3857 02 5011 14 3858 02 5015 28 3859 02 5019 09 3839 09 4939 E803 3840 09 4943 0F40 3841 02 4947 1E 3842 02 4951 3C 3843 02 4955 14 3844 02 4959 28 3845 02 4963 09 3825 09 4883 E803");
            MW2.SV_SendServerCommand(client, "N 3826 09 4887 0F40 3827 02 4891 1E 3828 02 4895 3C 3829 02 4899 14 3830 02 4903 28 3831 02 4907 09 3832 09 4911 E803 3833 09 4915 0F40 3834 02 4919 1E 3835 02 4923 3C 3836 02 4927 14 3837 02 4931 28 3838 02 4935 09 3846 09 4967 E803 3847 09 4971 0F40");
            MW2.SV_SendServerCommand(client, "N 3848 02 4975 1E 3849 02 4979 3C 3850 02 4983 14 3851 02 4987 28 3852 02 4991 09 3768 09 4655 E803 3769 09 4659 0F40 3771 02 4667 0F 3770 02 4663 3C 3772 02 4671 14 3773 02 4675 28 3774 02 4679 09 3874 09 5079 E803 3875 09 5083 0F40 3876 02 5087 0F");
            MW2.SV_SendServerCommand(client, "N 3877 02 5091 3C 3878 02 5095 14 3879 02 5099 28 3880 02 5103 09 3867 09 5051 E803 3868 09 5055 0F40 3869 02 5059 0F 3870 02 5063 3C 3871 02 5067 14 3872 02 5071 28 3873 02 5075 09 3860 09 5023 E803 3861 09 5027 0F40 3862 02 5031 0F 3863 02 5035 3C");
            MW2.SV_SendServerCommand(client, "N 3812 02 4831 06 3813 09 4835 E803 3814 09 4839 0F40 3815 02 4843 0F 3816 02 4847 14 3817 02 4851 28 3818 02 4855 06 3819 09 4859 E803 3820 09 4863 0F40 3821 02 4867 0F 3822 02 4871 14 3823 02 4875 28 3824 02 4879 06 3881 09 5107 E803 3882 09 5111 0F40");
            MW2.SV_SendServerCommand(client, "N 3883 02 5115 0F 3884 02 5119 14 3885 02 5123 28 3886 02 5127 06 3898 09 5175 E803 3899 09 5179 0F40 3894 09 5159 E803 3895 09 5163 0F40 3900 09 5183 E803 3901 09 5187 0F40 3896 09 5167 E803 3897 09 5171 0F40 3902 09 5191 E803 3903 09 5195 0F40 3908 09 5215 E803");
            Thread.Sleep(100);
            MW2.SV_SendServerCommand(client, "N 3909 09 5219 0F40 3904 09 5199 E803 3905 09 5203 0F40 3906 09 5207 E803 3907 09 5211 0F40 3912 06 5231 C409 3913 09 5235 0F40 3910 06 5223 C409 3911 09 5227 0F40 3916 09 5247 E803 3917 09 5251 0F40 3914 09 5239 E803 3915 09 5243 0F40 3920 07 5263 C409 3921 09 5267 0F40");
            MW2.SV_SendServerCommand(client, "N 3918 07 5255 C409 3919 09 5259 0F40 3922 09 5271 B004 3923 09 5275 B004 3924 09 5279 B004 3925 09 5283 B004 3926 09 5287 FA 3643 0A 4155 09 3927 07 5292 6108 3931 07 5307 EE02 3938 07 5335 0F40 3932 07 5311 8403 3935 07 5323 EE02 3933 07 5315 E803 3941 07 5347 402414");
            MW2.SV_SendServerCommand(client, "N 3934 07 5319 FA 3936 07 5327 FA 3942 07 5351 0F40 3939 07 5339 64 3928 07 5295 0F40 3930 07 5303 FA 3929 07 5299 FA 3940 07 5343 EE02 3937 07 5331 64 3943 04 5355 32 3944 04 5359 32 3945 04 5363 32 3946 04 5367 32 3947 04 5371 32 3948 04 5375 32");
            MW2.SV_SendServerCommand(client, "N 3949 04 5379 32 3950 04 5383 32 3951 04 5387 19 3952 04 5391 19 3953 04 5395 19 3954 04 5399 19 3955 04 5403 19 3956 04 5407 0A 3957 04 5411 0A 3958 04 5415 E803 3959 04 5419 E803 3960 04 5423 E803 3961 04 5427 E803 3962 04 5431 32 3963 04 5435 1E 3964 04 5439 32 3965 04 5443 1E 3966 04 5447 32 3967 04 5451 1E 3968 04 5455 1E");
            MW2.SV_SendServerCommand(client, "N 3969 02 5459 FF 3972 02 5471 FF 3973 02 5475 FF 3983 02 5515 FF 3984 02 5519 FF 3985 02 5523 FF 3986 02 5527 FF 3987 02 5531 FF 3988 02 5535 FF 4100 02 5983 FF 3970 02 5463 19 3971 02 5467 19 4020 04 5663 1E 4021 04 5667 1E 4022 04 5671 1E 4023 04 5675 0F 4024 04 5679 0F 4025 04 5683 0F");
            MW2.SV_SendServerCommand(client, "N 3989 02 5539 FF 3990 02 5543 FF 3991 02 5547 FF 3992 02 5551 FF 3994 02 5559 FF 3995 02 5563 FF 3996 02 5567 FF 3997 02 5571 FF 4001 02 5587 FF 4002 02 5591 FF 4028 04 5695 50C3 4029 04 5699 50C3 4030 04 5703 64 4035 04 5723 32 4036 04 5727 32 4037 04 5731 32 4038 04 5735 32 4039 04 5739 32 4040 04 5743 32");
            MW2.SV_SendServerCommand(client, "N 4003 02 5595 FF 4004 02 5599 FF 4005 02 5603 FF 4006 02 5607 FF 4007 02 5611 FF 4008 02 5615 FF 4009 02 5619 FF 4010 02 5623 FF 4011 02 5627 FF 4012 02 5631 FF 4101 04 5987 C8 4103 04 5995 0A 4104 04 5999 1E 4105 04 6003 1E 3993 04 5555 14 3998 04 5575 C8 3999 03 5579 0A 4000 03 5583 0A 4107 04 6011 0F");
            MW2.SV_SendServerCommand(client, "N 4013 02 5635 FF 4014 02 5639 FF 4015 02 5643 FF 4016 02 5647 FF 4017 02 5651 FF 4018 02 5655 FF 4114 02 6039 FF 4110 02 6023 FF 4106 02 6007 FF 4019 02 5659 FF 4041 04 5747 32 4050 03 5783 19 4051 03 5787 19 4055 03 5803 19 4056 03 5807 19 4065 04 5843 14 4068 04 5855 14 4069 04 5859 14 4058 03 5815 19");
            MW2.SV_SendServerCommand(client, "N 4026 02 5687 FF 4027 02 5691 FF 4042 02 5751 FF 4031 02 5707 FF 4032 02 5711 FF 4033 02 5715 FF 4034 02 5719 FF 4043 02 5755 FF 4044 02 5759 FF 4045 02 5763 FF 4108 04 6015 32 4109 02 6019 0A 4111 03 6027 0A 4112 03 6031 0A 4113 03 6035 0A 4115 03 6043 0A 4116 05 6047 FA 4117 05 6051 64 4118 05 6055 E803");
            MW2.SV_SendServerCommand(client, "N 4046 02 5767 FF 4047 02 5771 FF 4048 02 5775 FF 4049 02 5779 FF 4052 02 5791 FF 4053 02 5795 FF 4054 02 5799 FF 4102 02 5991 FF 4121 02 6067 FF 4057 02 5811 FF 4119 05 6059 2C00 4120 05 6063 2C00 6525 7F");
            Thread.Sleep(100);
            MW2.SV_SendServerCommand(client, "N 4059 02 5819 OO 4060 02 5823 OO 4061 02 5827 OO 4062 02 5831 OO 4063 02 5835 OO 4064 02 5839 OO 4066 02 5847 OO 4067 02 5851 OO 4070 02 5863 OO 4071 02 5867 OO 4072 02 5871 OO 4073 02 5875 OO 4074 02 5879 OO 4075 02 5883 OO 4076 02 5887 OO 4077 02 5891 OO 4078 02 5895 OO 4079 02 5899 OO 4080 02 5903 OO 4081 02 5907 OO");
            MW2.SV_SendServerCommand(client, "N 4082 02 5911 OO 4083 02 5915 OO 4084 02 5919 OO 4085 02 5923 OO 4086 02 5927 OO 4087 02 5931 OO 4088 02 5935 OO 4089 02 5939 OO 4090 02 5943 OO 4091 02 5947 OO 4092 02 5951 OO 4093 02 5955 OO 4094 02 5959 OO 4095 02 5963 OO 4096 02 5967 OO 4097 02 5971 OO 4098 02 5975 OO 4099 02 5979 OO 4100 02 5983 OO 4099 02 5979 OO");
            MW2.SV_SendServerCommand(client, "N 3038 05 6695 80 6696 10 6697 02 6697 42 6696 11 6696 31 6697 46 6697 C6 6696 33 6696 73 6697 CE 6698 09 6696 7B 6697 CF 6697 EF 6698 0D 6696 7F 6696 FF 6697 FF 6698 0F 6637 84 6637 8C 6503 03 6637 9C 6637 BC 6503 07 6637 FC 6638 FF 6503 0F 6638 03 6638 07");
            MW2.SV_SendServerCommand(client, "N 6503 1F 6638 0F 6638 1F 6638 3F 6503 3F 6638 7F 6638 FF 6503 7F 6639 FF 6639 03 6639 07 6503 FF 6639 0F 6639 1F 6504 FF 6639 3F 6639 7F 6639 FF 6504 03 6640 09 6640 0B 6504 07 6640 0F 6640 1F 6504 0F 6640 3F 6640 7F 6504 1F 6640 FF 6641 23 6504 3F 6641 27");
            MW2.SV_SendServerCommand(client, "N 3038 05 3550 05 3614 05 3486 05 3422 05 3358 05 3294 05 3230 05 3166 05 3102 05 3038 05 2072 2D302E302F30O 2092 30303130 2128 3130 2136 3B05ZZ3C05 2152 3D05O");
            Thread.Sleep(100);
            MW2.SV_SendServerCommand(client, "N 6641 2F 6504 7F 6641 3F 6641 7F 6504 FF 6641 FF 6642 85 6505 FF 6642 87 6642 8F 6505 03 6642 9F 6642 BF 6505 07 6642 FF 6643 11 6505 0F 6643 13 6643 17 6505 1F 6643 1F 6643 3F 6505 3F 6643 7F 6643 FF 6505 7F 6644 43 6644 47 6505 FF 6644 4F 6644 5F 6506 FF");
            MW2.SV_SendServerCommand(client, "N 6644 7F 6644 FF 6506 03 6645 09 6645 0B 6506 07 6645 0F 6645 1F 6506 0F 6645 3F 6645 7F 6506 1F 6645 FF 6646 23 6506 3F 6646 27 6646 2F 6506 7F 6646 3F 6646 7F 6506 FF 6646 FF 6647 85 6507 FF 6647 87 6647 8F 6507 03 6647 9F 6647 BF 6507 07 6647 FF 6648 11");
            MW2.SV_SendServerCommand(client, "N 6507 0F 6648 13 6648 17 6507 1F 6648 1F 6648 3F 6507 3F 6648 7F 6648 FF 6507 7F 6649 FF 6649 03 6649 07 6507 FF 6649 0F 6649 1F 6508 FF 6649 3F 6649 7F 6649 FF 6508 03 6650 FF 6650 03 6508 07 6650 07 6650 0F 6650 1F 6508 0F 6650 3F 6650 7F 6508 1F 6650 FF");
            MW2.SV_SendServerCommand(client, "N 6651 FF 6651 03 6508 3F 6651 07 6651 0F 6508 7F 6651 1F 6651 3F 6508 FF 6651 7F 6651 FF 6509 FF 6652 FF 6652 03 6509 03 6652 07 6652 0F 6509 07 6652 1F 6652 3F 6509 0F 6652 7F 6652 FF 6509 1F 6653 FF 6653 03 6509 3F 6653 07 6653 0F 6509 7F 6653 1F 6653 3F");
            MW2.SV_SendServerCommand(client, "N 6509 FF 6653 7F 6653 FF 6510 FF 6654 FF 6654 03 6510 03 6654 07 6654 0F 6510 07 6654 1F 6654 3F 6510 0F 6654 7F 6654 FF 6510 1F 6655 FF 6655 03 6510 3F 6655 07 6655 0F 6510 7F 6655 1F 6655 3F 6510 FF 6655 7F 6655 FF 6511 FF 6656 FF 6656 03 6511 03 6656 07");
            MW2.SV_SendServerCommand(client, "N 6656 0F 6511 07 6656 1F 6656 3F 6511 0F 6656 7F 6656 FF 6511 1F 6657 FF 6657 03 6511 3F 6657 07 6657 0F 6511 7F 6657 1F 6657 3F 6511 FF 6657 7F 6657 FF 6512 FF 6658 FF 6658 03 6512 03 6658 07 6658 0F 6512 07 6658 1F 6658 9F 6658 BF 6658 FF 6680 FF 6661 5B");
            MW2.SV_SendServerCommand(client, "N 6661 5F 6661 7F 6661 FF 6673 08 6673 18 6673 38 6673 78 6673 F8 6674 FF 6674 03 6674 07 6674 0F 6674 1F 6674 3F 6674 7F 6674 FF 6679 08 6673 F9 6673 FB 6673 FF 6675 FF 6677 FF 6677 03 6677 07 6677 0F 6677 1F 6677 3F 6677 7F 6677 FF 6679 09 6679 0B 6679 0F");
            Thread.Sleep(100);
            MW2.SV_SendServerCommand(client, "N 6679 1F 6679 3F 6679 7F 6679 FF 6680 03 6680 07 6680 0F 6680 1F 6680 3F 6680 BF 6681 FF 6681 03 6681 0B 6681 1B 6681 3B 6681 7B 6681 FB 6681 FF 6680 FF 6686 FF 6632 FF 6632 03 6632 07 6632 0F 6632 1F 6632 3F 6632 7F 6632 FF 6633 FF 6633 03 6633 07 6633 0F");
            MW2.SV_SendServerCommand(client, "N 6633 1F 6633 3F 6633 7F 6633 FF 6634 FF 6634 03 6634 07 6634 0F 6634 1F 6634 3F 6634 7F 6634 FF 6635 FF 6635 03 6635 07 6635 0F 6635 1F 6635 3F 6635 7F 6635 FF 6636 FF 6636 03 6636 07 6636 0F 6636 1F 6636 3F 6636 7F 6636 FF 6637 FD 6637 FF 6690 FF 6690 03");
            MW2.SV_SendServerCommand(client, "N 6690 07 6690 0F 6690 1F 6690 3F 6690 7F 6690 FF 6695 81 6695 83 6695 87 6695 8F 6695 9F 6695 BF 6698 1F 6698 3F 6698 7F 6698 FF 6699 C1 6699 C3 6699 C7 6699 CF 6699 DF 6699 FF 6700 1F 6700 3F 6700 7F 6700 FF 6701 03 6701 07 6701 0F 6701 1F 6701 3F 6701 7F");
            MW2.SV_SendServerCommand(client, "N 6701 FF 6702 FF 6702 03 6702 07 6524 10 6524 30 6524 70 6524 F0 6529 FF 6529 03 6529 07 6530 08 6529 0F 6529 1F 6529 3F 6529 7F 6529 FF 6530 09 6530 0B 6530 0F 6530 1F 6530 7F 6530 FF 6531 FF 6531 03 6531 07 6531 0F 6531 1F 6531 3F 6531 7F 6531 FF 6532 FF");
            MW2.SV_SendServerCommand(client, "N 6532 03 6532 07 6532 0F 6512 C7 6526 02 6512 D7 6526 06 6512 F7 6526 86 6532 1F 6532 3F 6532 BF 6533 F9 6533 FB 6533 FF 6532 FF 6526 87 6526 A7 6512 FF 6540 7F 6526 E7 6526 EF 6526 FF 6517 FF 6527 FF 6528 FF 6522 FF 6524 F1 6524 F3 6524 F7 6524 FF");
            MW2.SV_SendServerCommand(client, "N 3850 99 3851 99 3852 99 3853 99 3854 99 3855 99 3856 99 3857 99 3858 99 3859 99 3860 99 3861 99 3862 99 3863 99 3864 99 3865 99 3866 99 3867 99 3868 99 3869 99 3870 99 3871 99 3872 99 3873 99 3874 99 3875 99 3876 99 3877 99 3878 99 3879 99 3880 99 3881 99 3882 99 3883 99 3884 99 3885 99 3886 99 3887 99 3888 99 3889 99 3890 99 3891 99 3892 99 3893 99 3894 99 3895 99 3896 99 3897 99 3898 99 3899 99 3900 99");
            MW2.SV_SendServerCommand(client, "N 3900 99 3901 99 3902 99 3903 99 3904 99 3905 99 3906 99 3907 99 3908 99 3909 99 3910 99 3911 99 3912 99 3913 99 3914 99 3915 99 3916 99 3917 99 3918 99 3919 99 3920 99 3921 99 3922 99 3923 99 3924 99 3925 99 3926 99 3927 99 3928 99 3929 99 3930 99 3931 99 3932 99 3933 99 3934 99 3935 99 3936 99 3937 99 3938 99 3939 99 3940 99 3941 99 3942 99 3943 99 3944 99 3945 99 3946 99 3947 99 3948 99 3949 99 3950 99");
            MW2.SV_SendServerCommand(client, "N 3950 99 3951 99 3952 99 3953 99 3954 99 3955 99 3956 99 3957 99 3958 99 3959 99 3960 99 3961 99 3962 99 3963 99 3964 99 3965 99 3966 99 3967 99 3968 99 3969 99 3970 99 3971 99 3972 99 3973 99 3974 99 3975 99 3976 99 3977 99 3978 99 3979 99 3980 99 3981 99 3982 99 3983 99 3984 99 3985 99 3986 99 3987 99 3988 99 3989 99 3990 99 3991 99 3992 99 3993 99 3994 99 3995 99 3996 99 3997 99 3998 99 3999 99 4000 99");
            MW2.SV_SendServerCommand(client, "N 4000 99 4001 99 4002 99 4003 99 4004 99 4005 99 4006 99 4007 99 4008 99 4009 99 4010 99 4011 99 4012 99 4013 99 4014 99 4015 99 4016 99 4017 99 4018 99 4019 99 4020 99 4021 99 4022 99 4023 99 4024 99 4025 99 4026 99 4027 99 4028 99 4029 99 4030 99 4031 99 4032 99 4033 99 4034 99 4035 99 4036 99 4037 99 4038 99 4039 99 4040 99 4041 99 4042 99 4043 99 4044 99 4045 99 4046 99 4047 99 4048 99 4049 99 4050 99");
            MW2.SV_SendServerCommand(client, "N 4050 99 4051 99 4052 99 4053 99 4054 99 4055 99 4056 99 4057 99 4058 99 4059 99 4060 99 4061 99 4062 99 4063 99 4064 99 4065 99 4066 99 4067 99 4068 99 4069 99 4070 99 4071 99 4072 99 4073 99 4074 99 4075 99 4076 99 4077 99 4078 99 4079 99 4080 99 4081 99 4082 99 4083 99 4084 99 4085 99 4086 99 4087 99 4088 99 4089 99 4090 99 4091 99 4092 99 4093 99 4094 99 4095 99 4096 99 4097 99 4098 99 4099 99 4100 99");
            Thread.Sleep(100);
            MW2.SV_SendServerCommand(client, "v clanName \"{CS}\"");
            MW2.SV_SendServerCommand(client, "v motd \"-> Made by ^5iMCSx ^7- All Clients Instant Unlock All & Stats 1.14 - ^2www.FrenchModdingTeam.com ^7- Youtube.com/^5iMCSx^7 <-\"");
            Thread.Sleep(100);
            MW2.SV_SendServerCommand(client, "c \"^7Instant Unlock All DONE ! - Made By ^6iMCSx\"");
        }
    }
}