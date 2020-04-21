using System;
using System.Data;
using System.Threading;

namespace Snake
{
    class Program
    {
      static void Main(string[]args)
      {
            int[] xPosition =new int [50];
            xPosition[0] = 35;
            int[] yPosition =new int [50];
            yPosition[0] = 20;
            int appleX = 10;
            int appleY = 10;
            int applesEaten = 0;

            decimal gameSpeed = 150m;

            bool isGameOn = true;
            bool isWallHit = false;
            bool isAppleEaten = false;
            

            Random rand = new Random();

            Console.SetCursorPosition(xPosition[0], yPosition[0]);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine((char)200);

            setApplePositionOnScreen(rand, out appleX, out appleY);
            paintApple(appleX, appleY);

            buildWall();
            



            ConsoleKey command = Console.ReadKey().Key; /*Движение змейки*/
            do
            {
                switch (command)
                {
                    case ConsoleKey.LeftArrow:
                        Console.SetCursorPosition(xPosition[0], yPosition[0]);
                        Console.Write(" ");
                        xPosition[0]--;
                        break;
                    case ConsoleKey.UpArrow:
                        Console.SetCursorPosition(xPosition[0], yPosition[0]);
                        Console.Write(" ");
                        yPosition[0]--;
                        break;
                    case ConsoleKey.RightArrow:
                        Console.SetCursorPosition(xPosition[0], yPosition[0]);
                        Console.Write(" ");
                        xPosition[0]++;
                        break;
                    case ConsoleKey.DownArrow:
                        Console.SetCursorPosition(xPosition[0], yPosition[0]);
                        Console.Write(" ");
                        yPosition[0]++;
                        break;
                }

                paintSnake(applesEaten, xPosition, yPosition, out xPosition, out yPosition);


                



                isWallHit = DidSnakeHitWall(xPosition[0], yPosition[0]);

                if (isWallHit)
                {
                    isGameOn = false;
                    Console.SetCursorPosition(20, 20);
                    Console.WriteLine("         LOL YOU DIED   ");

                }

                isAppleEaten = AppleWasEaten(xPosition[0], yPosition[0], appleX, appleY);

                if(isAppleEaten)
                {
                    setApplePositionOnScreen(rand, out appleX, out appleY);
                    paintApple(appleX, appleY);
                    applesEaten++;
                    gameSpeed *= .925m;
                }

                

                if (Console.KeyAvailable) command = Console.ReadKey().Key;
                System.Threading.Thread.Sleep(Convert.ToInt32(gameSpeed));




            } while (isGameOn);

            

            





            Console.ReadKey();
      }

        private static void paintSnake(int applesEaten, int[] xPositionIn, int[] yPositionIn, out int[] xPositionOut, out int[] yPositionOut)
        {

            Console.SetCursorPosition(xPositionIn[0], yPositionIn[0]);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine((char)200);

            for(int i=1;i<applesEaten+1;i++)
            {
                Console.SetCursorPosition(xPositionIn[i], yPositionIn[i]);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("o");
            }


            Console.SetCursorPosition(xPositionIn[applesEaten+1], yPositionIn[applesEaten+1]);
            Console.WriteLine(" ");

            for(int i=applesEaten+1;i>0;i--)
            {
                xPositionIn[i] = xPositionIn[i - 1];
                yPositionIn[i] = yPositionIn[i - 1];
            }


            xPositionOut = xPositionIn;
            yPositionOut = yPositionIn;
        }

        private static bool AppleWasEaten(int xPosition, int yPosition, int appleX, int appleY)
        {
            if (xPosition == appleX && yPosition == appleY) return true;return false;
        }

        private static void paintApple(int appleX,int appleY)
        {
            Console.SetCursorPosition(appleX, appleY);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write((char)64);
        }

        private static void setApplePositionOnScreen(Random rand,out int appleX, out int appleY)
        {
            appleX = rand.Next(0 +2, 70 - 2);
            appleY = rand.Next(0 + 2, 40 - 2);
        }

        private static bool DidSnakeHitWall(int xPosition, int yPosition)
        {
            if (xPosition == 1 || xPosition == 70 || yPosition == 1 || yPosition == 40) return true; return false;

        }

       
        private static void buildWall() /*отрисовка поля*/
        {
            for (int i = 0; i < 41; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(1, i);
                Console.Write("#");
                Console.SetCursorPosition(70, i);
                Console.Write("#");

            }

            for(int i=1;i<70;i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(i,0);
                Console.Write("#");
                Console.SetCursorPosition(i, 40);
                Console.Write("#");
            }

        }
    }
}
