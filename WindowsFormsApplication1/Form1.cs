using Project.codes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics.Statistics;


namespace Project
{
    public partial class Form1 : Form
    {
        int k = 0;
        int küme = 0;
        int tt = 0;//0:nokta çizerken, 1: rastgelenokta oluştururken 2: çalışınca 3: refresh
        private static double[] datax;
        private static double[] datay;
        private static double[] dataxx;
        private static double[] datayy;
        private static double[] dataxk;
        private static double[] datayk;
        public static double[,] statistics =new double[100,5];
        public static double[,] statistics2 = new double[100, 5]; 
        Color[] colorSet = { Color.Red, Color.Blue, Color.Green, Color.Yellow,Color.Pink,Color.Black,Color.Orange,Color.AliceBlue,Color.Aqua,Color.Azure,Color.Beige};
        static List<Graph.Edge> mst;
        static List<Graph.Vertex> initials;
        static Graph graph = new Graph();
        static Graph graph3 = new Graph();
        private static Dictionary<int, List<Graph.Vertex>> clusters2;
        List<Point> points = new List<Point>();
        List<Point> pointsrandom = new List<Point>();

        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)//Panelde mouse click alıp koordinatlarını "points'e" eklemek.
        {
            
            points.Add(new Point(e.X,e.Y));
            panel1.Invalidate();
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e) //Paneli Boyamak için 
        {
             if (tt == 0)                                          //Alınan "Points" leri panelde boyalı göstermek ve "graph3" e eklemek için.
            {
                SolidBrush grayBrush2 = new SolidBrush(Color.Gray);
                foreach (Point p in points)
                {
                    e.Graphics.FillEllipse(grayBrush2, p.X, p.Y, 10, 10);
                    graph3.generateGraph(p.X, p.Y);                //"Graph3" içine noktaları ekleme
                }
                graph3.generateEdges();                            //"Graph3" ı oluşturma
            }

             if (tt == 1)                                          //"pointsrandom" oluşturma, panelde boyalı gösterme ve "graph" e eklemek için.
             {
                 Random r = new Random();
                 for (int i = 0; i < k; i++)
                 {
                     int xdimens = (Math.Abs(r.Next()) % 500);
                     int ydimens = (Math.Abs(r.Next()) % 300);
                     pointsrandom.Add(new Point(xdimens, ydimens));
                 }

                 SolidBrush grayBrush = new SolidBrush(Color.Gray);
                 foreach (Point p in pointsrandom)
                 {
                     e.Graphics.FillEllipse(grayBrush, p.X, p.Y, 10, 10);
                     graph.generateGraph(p.X, p.Y);                 //"Graph" içine noktaları ekleme
                 }
                 graph.generateEdges();                             //"Graph" ı oluşturma
                 pointsrandom.Clear();
             }

            if (tt == 2)                                             //Çalıştır tıklanınca noktaları kümelere göre boyama
            {
                for (int i = 0; i < clusters2.Count; i++)
                {
                    Color c=colorSet[i];                               //renk setinden renk alıyoruz (her bir küme için tekrarlanacak)
                    SolidBrush brush = new SolidBrush(c);
                    List<Graph.Vertex> elements = clusters2[i];         //Graph daki elemanları alıyoruz
                    datax = new double[elements.Count];                 //elemanlar kadar array oluştur
                    datay = new double[elements.Count];
                    for (int j = 0; j < elements.Count; j++)            
                    {
                        datax[j] = elements[j].getX();                  //elemanın X koordiatı al
                        datay[j] = elements[j].getY();                  //elemanın Y koordiatı al
                        e.Graphics.FillEllipse(brush, (int)elements[j].getX(), (int)elements[j].getY(), 10, 10);  //koordinatları boya
                        
                    }
                    double meanx=Statistics.Mean(datax);                //kümenin X ortasını bul
                    double meany = Statistics.Mean(datay);              //kümenin Y ortasını bul
                    SolidBrush brush2 = new SolidBrush(Color.Gray);
                    e.Graphics.DrawRectangle(new Pen(Color.Black, 3), (int)meanx, (int)meany, 10, 10);    //küme orta noktasını kare içine al               
                }
                tt = 0;
            }
           
        }

        private void button3_Click(object sender, EventArgs e)//Random tıklanınca.
        {
            tt = 1;
            k = Int32.Parse(textBox1.Text);
            panel1.Invalidate();  
            
        }

        private void button2_Click(object sender, EventArgs e) //Çalıştır tıklanınca.
        {
            küme = Convert.ToInt32(numericUpDown1.Value); //küme sayısı al
            Kruskal kruskal = new Kruskal(); //Kruskal sınıfı oluştur
            if (tt == 1) //Random "graph" için 
            {
                mst = kruskal.getMST(graph, küme); //Kruskal:graph ve küme sayısı alıp Minumum Spanning Tree oluşturur.
            }
            if (tt == 0) //Çizilen noktaların "graph3" için 
            {
                mst = kruskal.getMST(graph3, küme);
                
            }
            initials = kruskal.getInitials();      //Kruskal başlangıç noktaları al.
            Clustering c = new Clustering(mst);    //Clustering sınıfına "mst" veriyoruz. 
            c.clusterization(initials);            //Başlangıç noktaları veriyoruz.
            clusters2 = c.clusters;                //Oluşan kümeleri Dictinary "cluster2" ögesine atıyoruz. Paint ederken kullanacaz.
            int counter = 0;//aynı elemanları belirleme sayacı
            int counter2 = 0;//toplam elemen sayacı
            int kümenoktasayısı = 0;//küme içi eleman sayacı

            for (int i = 0; i < c.clusters.Count; i++)           //küme sayısı kadar dönecek
            {
                List<Project.codes.Graph.Vertex> elements2 = c.clusters[i];
                dataxk = new double[elements2.Count];
                datayk = new double[elements2.Count];
                int t = 0;
                for (int z = 0; z < dataxk.Length; z++)
                {
                    counter = 0;
                    for (int j = 0; j < elements2.Count; j++)
                    {
                        if (elements2[z].getX() == dataxk[j] && elements2[z].getY() == datayk[j]) //aynı nokta ise almamak için
                        {
                            counter++;
                        }
                    }
                    if (counter == 0)
                    {
                        dataxk[t] = elements2[z].getX();        //noktanın X koordinatı array a at.
                        datayk[t] = elements2[z].getY();        //noktanın Y koordinatı array a at.
                        t++;           //array counter arttır.
                        counter2++;    //toplam nokta sayısı counter arttır.
                    }
                }
            }

            richTextBox1.AppendText("Toplam Nokta Sayısı: " + counter2 + "\n"); //toplam nokta sayısı bas ekrana

            for (int i = 0; i < c.clusters.Count; i++)
            {
                List<Project.codes.Graph.Vertex> elements2 = c.clusters[i];
                dataxk = new double[elements2.Count];
                datayk = new double[elements2.Count];

                int t = 0;
                for (int z = 0; z < dataxk.Length; z++)
                {

                    counter = 0;
                    for (int j = 0; j < elements2.Count; j++)
                    {
                        if (elements2[z].getX() == dataxk[j] && elements2[z].getY() == datayk[j]) //aynı nokta ise almamak için
                        {
                            counter++;
                        }
                    }
                    if (counter == 0)
                    {
                        dataxk[t] = elements2[z].getX();        //noktanın x koordinatı array a at.
                        datayk[t] = elements2[z].getY();        //noktanın Y koordinatı array a at.
                        t++;
                        kümenoktasayısı++;                      // küme nokta sayısı counter arttır.
                    }
                }
                dataxx = new double[kümenoktasayısı]; //küme nokta sayısı kadar array oluştur.
                datayy = new double[kümenoktasayısı]; //küme nokta sayısı kadar array oluştur.
                for (int p = 0; p < kümenoktasayısı; p++) //noktaları array lere e aktar
                {
                    dataxx[p] = dataxk[p];
                    datayy[p] = datayk[p];
                }
                richTextBox1.AppendText(i + ". Küme" + "\n");
                richTextBox1.AppendText("  Nokta Sayısı: " + kümenoktasayısı + "\n");
                kümenoktasayısı = 0;
                double stdv;
                statistics2[i, 0] = Statistics.Mean(dataxx); //  "i." kümenin küme X orta noktası  
                statistics2[i, 1] = Statistics.Mean(datayy); //  "i." kümenin küme X orta noktası 
                statistics2[i, 2] = Statistics.Variance(dataxx); // "i." kümenin X e göre varyansı
                statistics2[i, 3] = Statistics.Variance(datayy); // "i." kümenin Y e göre varyansı
                stdv=statistics2[i, 2]+statistics2[i, 3]; //varyansları topluyoruz 2 koordinatlı olduğu için 
                statistics2[i, 4] = Math.Sqrt(stdv); //standart sapmayı toplam varyansın karekökünü alarak hesaplıyoruz (sapma bize orta noktaya olan öklid uzaklığını veriyor.)
                String ss, ss2;
                int xx = (int)Math.Ceiling(statistics2[i,0]); //sayıyı düzlüyoruz 
                ss = xx.ToString();
                xx = (int)Math.Ceiling(statistics2[i, 1]); 
                ss2 = xx.ToString();
                richTextBox1.AppendText("  Küme Merkezi: \n");
                richTextBox1.AppendText("["+ss+","+ss2+"]\n");//merkezi yazdır
                //richTextBox1.AppendText("  Varyans: \n"); 
                //xx = (int)Math.Ceiling(stdv);
                //ss = xx.ToString();
                //richTextBox1.AppendText(ss + "\n");  //varyans yazdır
                richTextBox1.AppendText("  Standart Sapma: \n");
                xx = (int)Math.Ceiling(statistics2[i, 4]);
                ss = xx.ToString();
                richTextBox1.AppendText(ss + "\n");//sapma yazdır
              }
 
            tt = 2;
            panel1.Invalidate();//Paneldeki noktaları boyamak için panel1_Paint methodunu çağırır.
        }

        private void button1_Click(object sender, EventArgs e) //refresh yapmak için
        {
            tt = 0;
            Graph graph2 = new Graph();
            graph = graph2; //Graph 0'lama
            graph3 = graph2;//Graph 0'lama
            points.Clear();//noktaları silme
            pointsrandom.Clear();//noktaları silme
            panel1.Invalidate();//paneli güncelle
            richTextBox1.Clear();//textbox silme

        }

    }
}
