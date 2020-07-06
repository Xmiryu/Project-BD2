using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using Microsoft.SqlServer.Server;
using System.Linq;
using System.Text;


namespace Dominanta
{
    //mediana
    [Serializable]
    [Microsoft.SqlServer.Server.SqlUserDefinedAggregate(
    Format.UserDefined, IsNullIfEmpty = true, MaxByteSize = 8000)]
    public struct Dominanta : IBinarySerialize
    {
        List<double> temp;
        public void Init()
        {
            this.temp = new List<double>();
        }
        public void Accumulate(SqlDouble Value)
        {
            if (!Value.IsNull)
            {
                this.temp.Add(Value.Value);
            }
        }
        public void Merge(Dominanta group)
        {
            this.temp.InsertRange(this.temp.Count, group.temp);
        }
        public SqlDouble Terminate()
        {
            //SqlDouble moda = SqlDouble.Null;
            //this.temp.Sort();
            int n_max=0, n_new;
            double last_number = this.temp[0], moda=this.temp[0];
            for(int i = 0; i<this.temp.Count; i++)
            {
                last_number = this.temp[i];
                n_new = 0;
                for (int j = 0; j < this.temp.Count; j++){
                    if (this.temp[j] == last_number)
                    {
                        n_new++;
                    }
                }
                
                if (n_new > n_max)
                {
                    moda = last_number;
                    n_max = n_new;
                }
            }

            return (SqlDouble)moda;
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