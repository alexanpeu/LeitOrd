using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leitura_Ordens {
    class Ordem {

        public Ordem(int num, double preco, double qtd, string cC, string cV, string hora, string agressor) {
            Num = num;
            Preco = preco;
            Qtd = qtd;
            CC = cC;
            CV = cV;
            Hora = hora;
            Agressor = agressor;
        }

        public int Num { get; set; }
        public double Preco { get; set; }
        public double Qtd { get; set; }
        public string CC { get; set; }
        public string CV { get; set; }
        public string Hora { get; set; }
        public string Agressor { get; set; }

        public int CompareTo(Ordem compareOrdem) {
            if (compareOrdem == null) {
                return 1;
            }
            else
                return this.Num.CompareTo(compareOrdem.Num);
        }


        public override string ToString() {
            return Num
                + "    -    "
                + Preco
                + "    -    "
                + Qtd
                + "    -    "
                + CC
                + "    -    "
                + CV
                + "    -    "
                + Hora
                + "    -    "
                + Agressor;

        }
    }
}
