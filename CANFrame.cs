using System;

namespace WpfComAp
{
    public class CANFrame
    {
        private int index;
        private long time;
        private string id;
        private bool    FD,         // Definuje ci je frame canFD
                        RRS_RTR,    // Remote Transmission Request
                        IDE,        // ID Extension (0 => 11bit ID, 1 => 29bit ID)
                        FDF,        // FD format indicator
                        BRS,        // Bit rate switch
                        ESI;        // Error State Indicator
        private int length;
        private string data;
        public bool hidden;

        public CANFrame(int index, long time, string id, bool rRS_RTR, bool iDE, bool fDF, bool bRS, int length, string data, bool esi, bool fD)
        {
            this.index=index;
            this.Time=time;
            this.Id=id;
            RRS_RTR1=rRS_RTR;
            IDE1=iDE;
            FDF1=fDF;
            BRS1=bRS;
            this.Length=length;
            this.Data=data;
            this.ESI=esi;
            FD=fD;
            hidden=false;
        }

        public CANFrame(string csvRow)
        {
            string[] rowSplit = csvRow.Split(';');

            this.FD = rowSplit[0].Equals("1");
            this.time = long.Parse(rowSplit[1]);
            this.id = rowSplit[2];
            this.RRS_RTR = rowSplit[3].Equals("1");
            this.IDE=rowSplit[4].Equals("1");
            this.FDF=rowSplit[5].Equals("1");
            this.BRS=rowSplit[6].Equals("1");
            this.ESI=rowSplit[7].Equals("1");
            setLength(int.Parse(rowSplit[8]));
            this.Data = rowSplit[9];


        }
        

        public void setLength(int dlc)
        {
            int dataLength;
            if (dlc <= 8)
            {
                dataLength = dlc;
            }
            else
            {
                switch (dlc)
                {
                    case 9:
                        dataLength = 12;
                        break;
                    case 10:
                        dataLength = 16;
                        break;
                    case 11:
                        dataLength = 20;
                        break;
                    case 12:
                        dataLength = 24;
                        break;
                    case 13:
                        dataLength = 32;
                        break;
                    case 14:
                        dataLength = 48;
                        break;
                    case 15:
                        dataLength = 64;
                        break;
                    default:
                        throw new Exception("Unexpected DLC value: " + dlc.ToString());
                }
                
            }
            this.length = dataLength;
        }

        public int Index { get => index; set => index=value; }
        public long Time { get => time; set => time=value; }
        public string Id { get => id; set => id=value; }
        public bool RRS_RTR1 { get => RRS_RTR; set => RRS_RTR=value; }
        public bool IDE1 { get => IDE; set => IDE=value; }
        public bool FDF1 { get => FDF; set => FDF=value; }
        public bool BRS1 { get => BRS; set => BRS=value; }
        public int Length { get => length; set => length=value; }
        public string Data { get => data; set => data=value; }
        public bool Esi { get => ESI; set => ESI=value; }
        public bool FD1 { get => FD; set => FD=value; }

        public string TimeHumanized() {
            return HumanizeTimeStatic(Time);
        }
        public static string HumanizeTimeStatic(long time) {
            long timeTicks = (time)/* - (long.Parse(startTime)*10)*/;
            TimeSpan t = TimeSpan.FromTicks(timeTicks);

            string formattedTime = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms:{4:D3}us",
                                    t.Hours,
                                    t.Minutes,
                                    t.Seconds,
                                    t.Milliseconds,
                                    (t.Ticks/10)%1000);
            return formattedTime;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
