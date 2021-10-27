namespace RateController
{
    public class PGN230
    {
        // vr data to RC
        // 0    128
        // 1    129
        // 2    source
        // 3    AGIO PGN 0xE6(230)
        // 4    length 5
        // 5    rate 0 %
        // 6    rate 1 %
        // 7    rate 2 %
        // 8    rate 3 %
        // 9    rate 4 %
        // 10   CRC

        private byte[] cRate = { 255, 255, 255, 255, 255 }; // 255 means no data
        private int Length = 11;

        public void ParseByteData(byte[] Data)
        {
            if (Data.Length == Length)
            {
                if (GoodCRC(Data))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        cRate[i] = Data[i + 5];
                    }
                }
            }
        }

        public byte Rate(byte RateID)
        {
            byte Result = 255;
            if (RateID < 5) Result = cRate[RateID];
            //Result = (byte)((RateID + 1) * 10);   // for testing
            return Result;
        }

        private bool GoodCRC(byte[] Data)
        {
            int CK = 0;
            for (int i = 2; i < Data.Length - 1; i++)
            {
                CK += Data[i];
            }
            return (byte)CK == Data[Data.Length - 1];
        }
    }
}