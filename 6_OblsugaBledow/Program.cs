using System;

namespace VI_OblsugaBledow
{
    class Program
    {
        static void Main(string[] args)
        {
            Turtle arthurTheTurtle = new Turtle
            {
                PlatformWidth = 0.0,
                PlatformHeigh = 10.0,
                MotorSpeed = 5.0
            };
            
            ShowPosition(arthurTheTurtle);

            try
            {
                //arthurTheTurtle.RunFor(0.0);

                arthurTheTurtle.LeftMotorState = MotorState.Running;
                arthurTheTurtle.RightMotorState = MotorState.Running;
                arthurTheTurtle.RunFor(2.0);

                //if (result != TurtleError.OK)
                //{
                //    HandleError(result);
                //    return;
                //}

                ShowPosition(arthurTheTurtle);

                arthurTheTurtle.RightMotorState = MotorState.Reversed;
                arthurTheTurtle.RunFor(Math.PI / 2.0);

                ShowPosition(arthurTheTurtle);

                arthurTheTurtle.RightMotorState = MotorState.Reversed;
                arthurTheTurtle.LeftMotorState = MotorState.Reversed;
                arthurTheTurtle.RunFor(5);

                ShowPosition(arthurTheTurtle);

                arthurTheTurtle.RightMotorState = MotorState.Running;
                arthurTheTurtle.RunFor(Math.PI / 4.0);

                ShowPosition(arthurTheTurtle);

                arthurTheTurtle.RightMotorState = MotorState.Reversed;
                arthurTheTurtle.LeftMotorState = MotorState.Reversed;
                arthurTheTurtle.RunFor(Math.Cos(Math.PI / 4.0));

                ShowPosition(arthurTheTurtle);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Robot zgłosił bład:");
                Console.WriteLine(e.Message);
            }
            catch (Exception e1)
            {
                Exception current = e1;
                while (current != null)
                {
                    Console.WriteLine(current.Message);
                    current = current.InnerException;
                }
            }
            finally
            {
                Console.WriteLine("Czekamy w bloku finally...");
                Console.ReadKey();
            }
        }

        //private static void HandleError(TurtleError result)
        //{
        //    Console.WriteLine("Robot zgłosił błąd {0}.", result);
        //    Console.ReadKey();
        //}

        private static void ShowPosition(Turtle arthurTheTurtle)
        {
            Console.WriteLine("Artur znajduje się w miejscu ({0}) i jest obrócony w kierunku {1:0.00} radianów.",
                arthurTheTurtle.CurrentPosition, 
                arthurTheTurtle.CurrentOrientation);
        }
    }
}
