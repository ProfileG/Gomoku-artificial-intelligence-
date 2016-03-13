using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gomoku_version_2
{
    public partial class Gomoku : Form
    {public String player_color = "white";
        public Board Board;
        public Gamelogic Gamelogic;


       



        public Gomoku()
        {
            InitializeComponent();
            Board = new Board();
            Gamelogic = new Gamelogic(Board);
            
        }

        private void Board_Ciz        (object sender, PaintEventArgs e) {
           
        Graphics j = e.Graphics;
        foreach (Point location in Board.Dinamic()) {
            Rectangle Rect = new Rectangle(location.X * 30 + 10, location.Y * 30 + 80, 30, 30);
            j.FillRectangle(Brushes.DarkGray, Rect);
            j.DrawRectangle(Pens.Black, Rect);

            if (Board.Position(location.X, location.Y) == "white")
            {



                Rectangle Rect2 = new Rectangle(location.X * 30 + 10, location.Y * 30 + 80, 30, 30);
                j.FillEllipse(Brushes.White, Rect2);
                j.DrawEllipse(Pens.Black, Rect2);
            }

            if (Board.Position(location.X, location.Y) == "black")
            {
                Rectangle Rect3 = new Rectangle(location.X * 30 + 10, location.Y * 30 + 80, 30, 30);
                j.FillEllipse(Brushes.Black, Rect3);
                j.DrawEllipse(Pens.Black, Rect3);

                
            }
        
        
        }
  

        
        }
        private void GoMoKu_MouseClick (object sender, MouseEventArgs e) {
            
            if (e.Button == MouseButtons.Left)
            {
                
                Point Ball = new Point((e.X - 10) / 30, (e.Y - 80) / 30);

                if (Board.Position(Ball.X, Ball.Y) == null)
                {
                    
                    ImputGame(Ball, player_color);
                    Point ImputStart = Gamelogic.MassiveSection(Board.Dinamic(), "black");
                    ImputGame(ImputStart, "black");
                }
          }
        
        }
        private void ImputGame            (Point location, string color)    {
            
            Board.Test[location.X,location.Y] = color;
            Refresh();
            if(Gamelogic.Done(location.X,location.Y,color)) {

                if(color == player_color)
                    MessageBox.Show("Вы победитель!");
                else
                    MessageBox.Show("Вы проиграли!");
                DialogResult result = MessageBox.Show("Продолжить?"," ", MessageBoxButtons.OKCancel);

                if (result == System.Windows.Forms.DialogResult.OK)


                { Board.Clear();
                Refresh();
                if (color == "white")
                    color = "black";

                else color = "white";
                }
                else
                {this.Close();}




                }

            }

        


        private void Form1_Load(object sender, EventArgs e)
        {
            
            
        }

       
    }

      public class Board 

    { public String [,] Test;
    ///public Button[,] Test; 
      public Board () {

Clear();


      }
      public String Position(int x,int y) {

          if (x < 0 || x > 14 || y < 0 || y > 14)
              return null;
              return this.Test[x, y];
          

         
          
          
      }
      public IEnumerable<Point> Dinamic() {
      
          for (int y =0; y <15; y++)
          {
              for (int x=0;x<15; x++)
              {

                  yield return new Point(x,y);

              }


          }


      }
      public void Clear(){
          this.Test = new String[15,15];
      }

    }
    
    public class Gamelogic
    {
        public Board board;

        public Gamelogic(Board board)
    
    {
            this.board =board;






    }
        
        public bool Done (int x, int y, String color) { 
        
        if (board.Position(x,y) == color && longMassive(new Point(x,y),color) >=4)
            return true;
        else
            return false;
        
        
        }
        
        public int longMassive (Point location, String color) { 
        
            int ver = MessageInt(location, -1 ,0, color) + MessageInt(location, 1 ,0, color);
            int gor = MessageInt(location, 0 ,-1, color) + MessageInt(location, 0,1, color);
            int leftCross =MessageInt(location, -1 ,-1, color) + MessageInt(location, 1 ,1, color);
            int rightCross = MessageInt(location, -1 ,1, color) + MessageInt(location, 1 ,-1, color);
        
        int[]ballInt = new int[] {ver,gor,leftCross,rightCross};
            int result = 0;
             for (int i = 0; i< ballInt.Length; i++) {result = Math.Max(result,ballInt[i]);}

           ////Test
            


            return result;
        
        } 
    
        public int MessageInt (Point Begins, int x, int y, String color) {
        int result = 0;

            for(int i  = 1;i<5; i++){
        Point target = Begins + new Size(i*x,i*y);
                if(board.Position(target.X,target.Y) != color){ 
                    
                    
                    
                    i = 5;}
                else 
                {result++;}
            }
                return result;
        
        }

        public Point MassiveSection ( IEnumerable<Point> Test, String color) {
        
        var result = new List<Point>();
            IComparable SetPosition = null;
            foreach (Point location in Test) {
                
                int Score1 = 0;
                int Score2 = 0;

                if(board.Position(location.X,location.Y) == null) {

                    IComparable Position_for_winner;
                    Score1 = 1 + longMassive(new Point(location.X,location.Y),color);
                    Score2 = 1+ longMassive(new Point(location.X,location.Y),"white");
                    if (Score1 == 5) 
                        Score1 = int.MaxValue;
                    Position_for_winner = Math.Max(Score1,Score2);

                    
                    if(Position_for_winner.CompareTo(SetPosition) > 0) {result.Clear();
                    SetPosition = Position_for_winner;
                    result.Add(location);}
                        
                }
            }
            return result[0];
        }
    }

}

///
