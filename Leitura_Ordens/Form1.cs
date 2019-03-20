using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;    //classe para ler aquivos 

namespace Leitura_Ordens {
    public partial class btnPararRelogio : Form {
        int numeroLinhas = 0;
        int tamanhoLista = 0;
        int tamanhoListaOR = 0;
        int linhaLida = 0;
        int linhaLidaOR = 0;
        double preco;
        double quantidade;
        string cc;
        string cv;
        string hora;
        string agressor;
        int numero;

        bool paraScrooll = false;
        DateTime horaInicioScrool;
        bool paraScroollOR = false;
        DateTime horaInicioScroolOR;

        int minuto;
        int segundo;
        
        List<Ordem> list = new List<Ordem>();
        List<Ordem> listOR = new List<Ordem>();

        DateTime relogio = new DateTime(2019,03,18,17, 08, 52);
       // TimeSpan incremento = new TimeSpan(0, 0, 1);



        public btnPararRelogio() {
            InitializeComponent();
        }
        
        private void label2_Click(object sender, EventArgs e) {

        }

        private void Form1_Load(object sender, EventArgs e) {
           
        }

        private void button1_Click(object sender, EventArgs e) {
            LeArquivo.Enabled = false;
            string nomeArquivo = txtNomeArquivo.Text;
            TextReader arquivo = new StreamReader(txtNomeArquivo.Text);
            numeroLinhas = System.IO.File.ReadAllLines(@nomeArquivo).Length;
            //numeroLinhas = 1000;
            label2.Text = Convert.ToString(numeroLinhas);


            string[] valor = new string[numeroLinhas];
            string nomeAtivo = arquivo.ReadLine();
            string cabecalhoArquivo = arquivo.ReadLine();
            for (int i=0; i<numeroLinhas-2;i++) {
                valor[i]= arquivo.ReadLine();
                if (valor[i] != "") {
                   // listBox1.Items.Add(valor[i]);
                    string[] linha = valor[i].Split('"');

                    string preco01 = linha[1];
                    preco = double.Parse(preco01.Substring(0, 8));
                    quantidade = double.Parse(linha[3]);
                    cc = linha[7];
                    cv = linha[11];
                    hora = linha[15];
                    numero = int.Parse(linha[19]);
                    agressor = linha[23];
                    list.Add(new Ordem(numero, preco, quantidade, cc, cv, hora, agressor));
                   /* listBox1.Items.Add(list[i].Preco + "    -" + list[i].Qtd + "    -" + list[i].CC +
                                       "    -" + list[i].CV + "    -" + list[i].Hora + "    -" + list[i].Num +
                                       "    -" + list[i].Agressor);  */
                }
                
            }
            arquivo.Close();

            tamanhoLista = list.Count;
            linhaLida = 1;
            //            listBox1.Items.Add(tamanhoLista);
            CalculaOrOriginal();
            tamanhoListaOR = listOR.Count;
            linhaLidaOR = 1;
            string horaInicio = list[tamanhoLista - linhaLida].Hora;
            horaInicio = "2019/03/18 " + horaInicio;
            relogio = DateTime.Parse(horaInicio);

            timer1.Enabled = true;
            tOrdens.Enabled = true;
           // gOrdens.DataSource = list;
            

          /*  foreach (Ordem obj in list) {
                listBox1.Items.Add(obj);
            } */
        }

        private void timer1_Tick(object sender, EventArgs e) {
            relogio = relogio.AddSeconds(1);
            lblRelogio.Text = Convert.ToString(relogio);

            if (paraScrooll == true) {
                if (relogio >= horaInicioScrool)
                    paraScrooll = false;

            }
            if (paraScroollOR == true) {
                if (relogio >= horaInicioScroolOR)
                    paraScroollOR = false;

            }

        }

        private void button1_Click_1(object sender, EventArgs e) {
            timer1.Enabled = false;

        }

        private void tOrdens_Tick(object sender, EventArgs e) {
            if (linhaLida < tamanhoLista) {

                string horaS = list[tamanhoLista - linhaLida].Hora;
                horaS = "2019/03/18 " + horaS;
                DateTime ordem = DateTime.Parse(horaS);
                


                if (relogio >= ordem) {
                   // listBox2.Items.Add(list[tamanhoLista - linhaLida]);
                    linhaLida++;
                  //  listBox2.SetSelected(linhaLida, true);
                    string agre;
                    if (list[tamanhoLista - linhaLida].Agressor == "Comprador")
                        agre = "C";
                    else if (list[tamanhoLista - linhaLida].Agressor == "Vendedor")
                        agre = "V";
                    else if (list[tamanhoLista - linhaLida].Agressor == "Cross")
                        agre = "CR";
                    else if (list[tamanhoLista - linhaLida].Agressor == "Leilão")
                        agre = "L";


                    gOrdens.Rows.Add(list[tamanhoLista - linhaLida].Hora,
                        list[tamanhoLista - linhaLida].Preco.ToString(), 
                        list[tamanhoLista - linhaLida].Qtd.ToString(),
                        list[tamanhoLista - linhaLida].CC, list[tamanhoLista - linhaLida].CV,
                        list[tamanhoLista-linhaLida].Agressor,
                        list[tamanhoLista - linhaLida].Num.ToString());
                    if (paraScrooll == false) {
                        gOrdens.FirstDisplayedScrollingRowIndex = gOrdens.RowCount - 1;
                        horaInicioScrool = DateTime.Parse(horaS);
                        horaInicioScrool = horaInicioScrool.AddSeconds(30);
                    }
                }
                //gOrdens.CurrentCell = gOrdens.Rows[gOrdens.Rows.Count - 1].Cells[1];
                

            }
            if (linhaLidaOR < tamanhoListaOR) {

                string horaSOR = listOR[0 + linhaLidaOR].Hora;
                horaSOR = "2019/03/18 " + horaSOR;
                DateTime ordemOR = DateTime.Parse(horaSOR);
                if (relogio >= ordemOR) {
                    linhaLidaOR++;

                    gOrdensOR.Rows.Add(listOR[0 + linhaLidaOR].Hora,
                        listOR[0 + linhaLidaOR].Preco.ToString(),
                        listOR[0 + linhaLidaOR].Qtd.ToString(),
                        listOR[0 + linhaLidaOR].CC, listOR[0 + linhaLidaOR].CV,
                        listOR[0 + linhaLidaOR].Agressor,
                        listOR[0 + linhaLidaOR].Num.ToString());
                    if (paraScroollOR == false) {
                        gOrdensOR.FirstDisplayedScrollingRowIndex = gOrdensOR.RowCount - 1;
                        horaInicioScroolOR = DateTime.Parse(horaSOR);
                        horaInicioScroolOR = horaInicioScroolOR.AddSeconds(30);
                    }
                }

            }

        }

        private void btnContinuarRelogio_Click(object sender, EventArgs e) {
            timer1.Enabled = true;
        }

        private void gOrdens_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            paraScrooll = true;
        }

        private void gOrdens_Click(object sender, EventArgs e) {
            paraScrooll = true;
        }


        //função calcula ordem original
        public void CalculaOrOriginal() {
            double quantOrdemOriginal = 0;
            int registroOr = list.Count;
            double precoInicialBoleta = 0;
            double precoFinalBoleta = 0;
            bool boletada = false;

            int tamanhoListaO = list.Count;


            double oPreco;
            double oQuantidade;
            string oCC;
            string oCV;
            string oHora;
            int oNumero;
            string oAgressor;

            double oPrecoProx ;
            double oQuantidadeProx;
            string oCCProx;
            string oCVProx;
            string oHoraProx;
            int oNumeroProx;
            string oAgressorProx;

            for (int i = tamanhoListaO - 1; i >= 1; i--) {

                oPreco = list[i].Preco;
                oQuantidade = list[i].Qtd;
                oCC = list[i].CC;
                oCV = list[i].CV;
                oHora = list[i].Hora;
                oNumero = list[i].Num;
                oAgressor = list[i].Agressor;

                oPrecoProx = list[i - 1].Preco;
                oQuantidadeProx = list[i - 1].Qtd;
                oCCProx = list[i - 1].CC;
                oCVProx = list[i - 1].CV;
                oHoraProx = list[i - 1].Hora;
                oNumeroProx = list[i - 1].Num;
                oAgressorProx = list[i - 1].Agressor;

                if (oAgressor == "Leil�o") {
                    listOR.Add(new Ordem(oNumero, oPreco, oQuantidade, oCC, oCV, oHora, oAgressor));
                    quantOrdemOriginal = oQuantidadeProx;
                    registroOr = registroOr - 1;
                } //fim do if verifica se tá em leilao
                else {
                        if (oHora == oHoraProx && oAgressor == "Comprador" && oAgressorProx == "Comprador" && oCC == oCCProx) {
                            quantOrdemOriginal = quantOrdemOriginal + oQuantidadeProx;
                            if (boletada == false) {
                                 precoInicialBoleta = oPreco;
                                 boletada = true;
                             }
                        }
                        else if (oHora == oHoraProx && oAgressor == "Vendedor" && oAgressorProx == "Vendedor" && oCV == oCVProx) {
                            quantOrdemOriginal = quantOrdemOriginal + oQuantidadeProx;
                            if (boletada == false) {
                                precoInicialBoleta = oPreco;
                                boletada = true;
                            }
                        }
                        else if (oHora == oHoraProx && oAgressor == "Cross" && oAgressorProx == "Cross" && oCV == oCVProx) {
                            quantOrdemOriginal = quantOrdemOriginal + oQuantidadeProx;
                            if (boletada == false) {
                                precoInicialBoleta = oPreco;
                                boletada = true;
                            }
                        }
                        else if (oHora == oHoraProx && oAgressor == oAgressorProx && oCC != oCCProx) {
                            string agrC, agrV;
                            if (oAgressor == "Comprador") {
                                agrC = oCC;
                                agrV = "";
                            }
                            else if (oAgressor == "Vendedor") {
                                agrC = "";
                                agrV = oCV;
                            }
                            else {
                                agrC = oCC;
                                agrV = oCV;
                            }
                            
                            precoFinalBoleta = oPreco;
                            if (oAgressor == "Vendedor" && precoInicialBoleta > precoFinalBoleta && precoInicialBoleta > 0) {
                                oAgressor = oAgressor + "**";
                            }
                            if (oAgressor == "Comprador" && precoInicialBoleta < precoFinalBoleta && precoInicialBoleta > 0) {
                                oAgressor = oAgressor + "**";
                            }

                            listOR.Add(new Ordem(oNumero, oPreco, quantOrdemOriginal, agrC, agrV, oHora, oAgressor));
                            quantOrdemOriginal = oQuantidadeProx;
                            boletada = false;
                            precoInicialBoleta = 0;
                            precoFinalBoleta = 0;
                            registroOr = registroOr - 1;
                        }
                        else if (oHora == oHoraProx && oAgressor == oAgressorProx && oCV != oCVProx) {
                            string agrC, agrV;
                            if (oAgressor == "Comprador") {
                                agrC = oCC;
                                agrV = "";
                            }
                            else if (oAgressor == "Vendedor") {
                                agrC = "";
                                agrV = oCV;
                            }
                            else {
                                agrC = oCC;
                                agrV = oCV;
                            }
                            
                            precoFinalBoleta = oPreco;
                            if (oAgressor == "Vendedor" && precoInicialBoleta > precoFinalBoleta && precoInicialBoleta > 0) {
                                oAgressor = oAgressor + "**";
                            }
                            if (oAgressor == "Comprador" && precoInicialBoleta < precoFinalBoleta && precoInicialBoleta > 0) {
                                oAgressor = oAgressor + "**";
                            }

                            listOR.Add(new Ordem(oNumero, oPreco, quantOrdemOriginal, agrC, agrV, oHora, oAgressor));
                            quantOrdemOriginal = oQuantidadeProx;
                            boletada = false;
                            precoInicialBoleta = 0;
                            precoFinalBoleta = 0;
                            registroOr = registroOr - 1;
                        }
                        else if (oHora == oHoraProx && oAgressor != oAgressorProx) {
                            string agrC, agrV;
                            if (oAgressor == "Comprador") {
                                agrC = oCC;
                                agrV = "";
                            }
                            else if (oAgressor == "Vendedor") {
                                agrC = "";
                                agrV = oCV;
                            }
                            else {
                                agrC = oCC;
                                agrV = oCV;
                            }
                            
                            precoFinalBoleta = oPreco;
                            if (oAgressor == "Vendedor" && precoInicialBoleta > precoFinalBoleta && precoInicialBoleta > 0) {
                                oAgressor = oAgressor + "**";
                            }
                            if (oAgressor == "Comprador" && precoInicialBoleta < precoFinalBoleta && precoInicialBoleta > 0) {
                                oAgressor = oAgressor + "**";
                            }

                            listOR.Add(new Ordem(oNumero, oPreco, quantOrdemOriginal, agrC, agrV, oHora, oAgressor));
                            quantOrdemOriginal = oQuantidadeProx;
                            boletada = false;
                            precoInicialBoleta = 0;
                            precoFinalBoleta = 0;
                            registroOr = registroOr - 1;
                        }
                        else if (oHora != oHoraProx) {
                            string agrC, agrV;
                            if (oAgressor == "Comprador") {
                                agrC = oCC;
                                agrV = "";
                            }
                            else if (oAgressor == "Vendedor") {
                                agrC = "";
                                agrV = oCV;
                            }
                            else {
                                agrC = oCC;
                                agrV = oCV;
                            }
                            
                            precoFinalBoleta = oPreco;
                            if (oAgressor == "Vendedor" && precoInicialBoleta > precoFinalBoleta && precoInicialBoleta > 0) {
                                oAgressor = oAgressor + "**";
                            }
                            if (oAgressor == "Comprador" && precoInicialBoleta < precoFinalBoleta && precoInicialBoleta > 0) {
                                oAgressor = oAgressor + "**";
                            }

                            listOR.Add(new Ordem(oNumero, oPreco, quantOrdemOriginal, agrC, agrV, oHora, oAgressor));
                            quantOrdemOriginal = oQuantidadeProx;
                            boletada = false;
                            precoInicialBoleta = 0;
                            precoFinalBoleta = 0;
                            registroOr = registroOr - 1;
                        }

                } //fim do if se não é leilão


            } //fim do if contator principal

              //  foreach (Ordem obj in listOR) {
                   // listBox1.Items.Add(obj);
              //  }

            //for (int i = listOR.Count-1; i>= 1;i--) {
            //    listBox1.Items.Add(listOR[i].Preco + "    -" + listOR[i].Qtd + "    -" + listOR[i].CC +
           //                            "    -" + listOR[i].CV + "    -" + listOR[i].Hora + "    -" + listOR[i].Num +
            //                           "    -" + listOR[i].Agressor);
           // }
            
                    
                    
         } //fim da função calcula ordem original

        private void gOrdensOR_Click(object sender, EventArgs e) {
            paraScroollOR = true;
        }

        private void tOrdensG_Tick(object sender, EventArgs e) {

        }
    }


 
}
