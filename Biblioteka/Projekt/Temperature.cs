using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using Microsoft.SqlServer.Server;
using System.Linq;
using System.Text;

namespace Temperature
{
        //ile razy temp ponizej zera
    [Serializable]
    [Microsoft.SqlServer.Server.SqlUserDefinedAggregate(Format.Native)]
    public struct uda_CountOfNegatives
    {
        private SqlInt32 iNumOfNegatives;
        public void Init()
        {
            this.iNumOfNegatives = 0;
        }
        public void Accumulate(SqlInt32 Value)
        {
            if ((Value) < 0)
            {
                this.iNumOfNegatives += 1;
            }
        }
        public void Merge(uda_CountOfNegatives Group)
        {
            this.iNumOfNegatives += Group.iNumOfNegatives;
        }
        public SqlInt32 Terminate()
        {
            return ((SqlInt32)this.iNumOfNegatives);
        }
    };

        //ile razy temp powyzej temp wegetacyjnej
    [Serializable]
    [Microsoft.SqlServer.Server.SqlUserDefinedAggregate(Format.Native)]
    public struct uda_CountOfVegetation
    {
        private SqlInt32 iNumOfVegetation;
        public void Init()
        {
            this.iNumOfVegetation = 0;
        }
        public void Accumulate(SqlInt32 Value)
        {
            if ((Value) > 5)    //powyzej temperatury wegetacji traw
            {
                this.iNumOfVegetation += 1;
            }
        }
        public void Merge(uda_CountOfVegetation Group)
        {
            this.iNumOfVegetation += Group.iNumOfVegetation;
        }
        public SqlInt32 Terminate()
        {
            return ((SqlInt32)this.iNumOfVegetation);
        }
    };

        //ile dni w zakresie min max
    [Serializable]
    [Microsoft.SqlServer.Server.SqlUserDefinedAggregate(Format.Native)]
    public struct uda_CountOfRange
    {
        private SqlInt32 iNumRange;
        public void Init()
        {
            this.iNumRange = 0;
        }
        public void Accumulate(SqlInt32 Value, SqlInt32 min, SqlInt32 max)
        {
            if ((Value) < max && (Value) > min)
            {
                this.iNumRange += 1;
            }
        }
        public void Merge(uda_CountOfRange Group)
        {
            this.iNumRange += Group.iNumRange;
        }
        public SqlInt32 Terminate()
        {
            return ((SqlInt32)this.iNumRange);
        }
    };

        //mediana
    [Serializable]
    [Microsoft.SqlServer.Server.SqlUserDefinedAggregate(
    Format.UserDefined, IsNullIfEmpty = true, MaxByteSize = 8000)]
    [StructLayout(LayoutKind.Sequential)]
    public struct Median : IBinarySerialize
    {
        List<double> temp;
        public void Init()
        {
            this.temp = new List<double>();
        }
        public void Accumulate(SqlDouble number)
        {
            if (!number.IsNull)
            {
                this.temp.Add(number.Value);
            }
        }
        public void Merge(Median group)
        {
            this.temp.InsertRange(this.temp.Count, group.temp);
        }
        public SqlDouble Terminate()
        {
            SqlDouble result = SqlDouble.Null;
            this.temp.Sort();
            int first, second;
            if (this.temp.Count % 2 == 1)
            {
                first = this.temp.Count / 2;
                second = first;
            }
            else
            {
                first = this.temp.Count / 2;
                second = first + 1;
            }
            if (this.temp.Count > 0)
            {
                result = (SqlDouble)(this.temp[first] + this.temp[second]) / 2.0;
            }
            return result;
        }
        #region IBinarySerialize Members
        // Własna metoda do wczytywania serializowanych danych.
        public void Read(System.IO.BinaryReader r)
        {
            this.temp = new List<double>();
            int j = r.ReadInt32();
            for (int i = 0; i < j; i++)
            {
                this.temp.Add(r.ReadDouble());
            }
        }
        // Własna metoda do zapisywania serializowanych danych.
        public void Write(System.IO.BinaryWriter w)
        {
            w.Write(this.temp.Count);
            foreach (double d in this.temp)
            {
                w.Write(d);
            }
        }
        #endregion
    }
}