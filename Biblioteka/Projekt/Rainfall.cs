using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using Microsoft.SqlServer.Server;

namespace Rainfall
{
        //ilosv opadow w przedziale czasu
    [Serializable]
    [Microsoft.SqlServer.Server.SqlUserDefinedAggregate(Format.Native)]
    public struct uda_SumOfRange
    {
        private SqlDouble iSumRange;
        public void Init()
        {
            this.iSumRange = 0;
        }
        public void Accumulate(SqlDouble Value, DateTime Date, DateTime date1, DateTime date2)
        {
            if (DateTime.Compare(date1, Date) < 0 && DateTime.Compare(Date, date2)<0)
            {
                this.iSumRange += Value;
            }
        }
        public void Merge(uda_SumOfRange Group)
        {
            this.iSumRange += Group.iSumRange;
        }
        public SqlDouble Terminate()
        {
            return ((SqlDouble)this.iSumRange);
        }
    };

        //srednia geometryczna
    [Serializable]
    [Microsoft.SqlServer.Server.SqlUserDefinedAggregate(Format.Native)]
    public struct uda_GeoAvg
    {
        private SqlDouble iGeo;
        private SqlInt32 iCount;

        public void Init()
        {
            this.iGeo = 1;
            this.iCount = 0;
        }

        public void Accumulate(SqlDouble Value)
        {
            if (Value>0)
            {
                this.iGeo *= Value;
                this.iCount += 1;
            }
        }

        public void Merge(uda_GeoAvg Group)
        {
            this.iGeo *= Group.iGeo;
            this.iCount += Group.iCount;
        }

        public SqlDouble Terminate()
        {
            if (iCount == 0)
            {
                return SqlDouble.Null;
            }
            double p = 1.0/(double)this.iCount;
            return Math.Pow((double)this.iGeo, p);
        }

    };

    //procent sniegu
    [Serializable]
    [Microsoft.SqlServer.Server.SqlUserDefinedAggregate(Format.Native)]
    public struct uda_SnowPercent
    {
        private SqlDouble iAllRain;
        private SqlDouble iSnow;

        public void Init()
        {
            this.iAllRain = 0;
            this.iSnow = 0;
        }

        public void Accumulate(SqlDouble Value, SqlInt32 bit)
        {
            if (bit == 1)
            {
                this.iSnow += Value;
            }
            this.iAllRain += Value;
        }

        public void Merge(uda_SnowPercent Group)
        {
            this.iSnow += Group.iSnow;
            this.iAllRain += Group.iAllRain;
        }

        public SqlDouble Terminate()
        {
            if (iAllRain == 0)
            {
                return SqlDouble.Null;
            }
            return 100*this.iSnow/this.iAllRain;
        }

    };

}
