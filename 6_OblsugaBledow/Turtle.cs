using System;
using System.Windows;

namespace VI_OblsugaBledow
{
    class Turtle
    {
        private double platformWidth;

        public Turtle()
        {
            //EnsurePlatformSize();
        }

        public double PlatformWidth
        {
            get { return platformWidth; }
            set
            {
                platformWidth = value;
                //EnsurePlatformSize();
            }
        }

        //private void EnsurePlatformSize()
        //{
        //    if (PlatformWidth < 1.0)
        //    {
        //        PlatformWidth = 1.0;
        //    }
        //    if (PlatformWidth > 10.0)
        //    {
        //        PlatformWidth = 10.0;
        //    }
        //    if (PlatformHeigh < 1.0)
        //    {
        //        PlatformHeigh = 1.0;
        //    }
        //    if (PlatformHeigh > 10.0)
        //    {
        //        PlatformHeigh = 10.0;
        //    }
        //}

        private double platformHeigh;

        public double PlatformHeigh
        {
            get { return platformHeigh; }
            set
            {
                platformHeigh = value;
                //EnsurePlatformSize();
            }
        }

        public double MotorSpeed { get; set; }

        public MotorState LeftMotorState { get; set; }

        public MotorState RightMotorState { get; set; }

        public Point CurrentPosition { get; private set; }

        public double CurrentOrientation { get; private set; }

        public void RunFor(double duration)
        {
            if (duration <= double.Epsilon)
            {
                throw new ArgumentException("Robot musi jechac przez okres dluzszy od 0.", "duration");
            }
            try
            {
                if (LeftMotorState == MotorState.Stopped && RightMotorState == MotorState.Stopped)
                {
                    return;
                }

                if ((LeftMotorState == MotorState.Running && RightMotorState == MotorState.Running)
                    || (LeftMotorState == MotorState.Reversed && RightMotorState == MotorState.Reversed))
                {
                    Drive(duration);
                    return;
                }

                if ((LeftMotorState == MotorState.Running && RightMotorState == MotorState.Reversed)
                    || (LeftMotorState == MotorState.Reversed && RightMotorState == MotorState.Running))
                {
                    Rotate(duration);
                }
            }
            catch (InvalidOperationException iox)
            {
                throw new Exception("Jakiś problem z robotem...", iox);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Informacja do dziennika: " + ex.Message);
                throw;
            }
            finally
            {
                Console.WriteLine("W bloku finally w klasie Turtle.");
            }
        }

        private void Drive(double duration)
        {
            double d = duration * MotorSpeed;
            if (LeftMotorState == MotorState.Reversed)
            {
                d *= -1.0;
            }

            double deltaX = d * Math.Sin(CurrentOrientation);
            double deltaY = d * Math.Cos(CurrentOrientation);

            CurrentPosition = new Point(CurrentPosition.X + deltaX, CurrentPosition.Y + deltaY);
        }

        private void Rotate(double duration)
        {
            if (PlatformWidth <= 0.0)
            {
                throw new InvalidOperationException("Właściwości PlatformWidth nalezy przypisać wartość > 0.0");
            }

            double circum = Math.PI * PlatformWidth;
            double d = duration * MotorSpeed;

            if (LeftMotorState == MotorState.Reversed)
            {
                d *= -1.0;
            }

            double proportionOfWholeCircle = d / circum;
            CurrentOrientation = CurrentOrientation + (Math.PI * 2.0 * proportionOfWholeCircle);
        }
    }

    enum MotorState
    {
        Stopped,
        Running,
        Reversed
    }

    //enum TurtleError
    //{
    //    OK,
    //    RotateError,
    //    MotorStateError
    //}
}