
namespace MartialRobotsApp
{

    internal class Robot
    {
        public enum Orientations { North, East, South, West }

        public enum Direccions { Forward, Right, Left }

        public Coordinates Coordinate { get; set; }
        public Coordinates Surface { get; set; }
        public Orientations Orientation { get; set; }
        public bool IsLost { get; set; } = false;
        public string LostText { get; set; } = string.Empty;
        public Coordinates LastCoordinateBeforeLost { get; set; }

        public Robot(Coordinates coordinate, Orientations orientation, Coordinates surface)
        {
            Coordinate = coordinate;
            Orientation = orientation;
            IsLost = false;
            LastCoordinateBeforeLost = new Coordinates(0, 0);
            CheckMaximumSurface(surface);
        }

        public void Move(string moveActions)
        {
            var directions = moveActions.ToCharArray();

            if (moveActions.Length > 50)
            {
                directions = moveActions.Substring(0, 50).ToCharArray();
            }

            foreach (var direction in directions)
            {
                if (direction == 'R' || direction == 'L')
                {
                    Rotate(direction);
                }
                else if (direction == 'F')
                {
                    CheckIfLost();
                    MoveFoward(Coordinate);
                }
            }
        }

        public void MoveFoward(Coordinates position)
        {
            if (Orientation == Orientations.North)
            {
                position.LatitudeY += 1;
            }
            else if (Orientation == Orientations.South)
            {
                position.LatitudeY -= 1;
            }
            else if (Orientation == Orientations.West)
            {
                position.LongitudeX -= 1;
            }
            else if (Orientation == Orientations.East)
            {
                position.LongitudeX += 1;
            }
        }

        public void Rotate(char direction)
        {
            if (Orientation == Orientations.North)
            {
                if (direction == 'R')
                {
                    RotateToEast();
                }
                else
                {
                    RotateToWest();
                }
            }
            else if (Orientation == Orientations.East)
            {
                if (direction == 'R')
                {
                    RotateToSouth();
                }
                else
                {
                    RotateToNorth();
                }
            }
            else if (Orientation == Orientations.South)
            {
                if (direction == 'R')
                {
                    RotateToWest();
                }
                else
                {
                    RotateToEast();
                }
            }
            else if (Orientation == Orientations.West)
            {
                if (direction == 'R')
                {
                    RotateToNorth();
                }
                else
                {
                    RotateToSouth();
                }
            }
        }

        public void RotateToNorth()
        {
            Orientation = Orientations.North;
        }

        public void RotateToEast()
        {
            Orientation = Orientations.East;
        }

        public void RotateToSouth()
        {
            Orientation = Orientations.South;
        }

        public void RotateToWest()
        {
            Orientation = Orientations.West;
        }

        private void CheckIfLost()
        {
            if (!IsLost && (Coordinate.LatitudeY < 0 || Coordinate.LatitudeY > Surface.LatitudeY ||
                Coordinate.LongitudeX < 0 || Coordinate.LongitudeX > Surface.LongitudeX))
            {
                IsLost = true;
                LostText = "LOST";
                LastCoordinateBeforeLost.LongitudeX = Coordinate.LongitudeX;
                LastCoordinateBeforeLost.LatitudeY = Coordinate.LatitudeY;
            }
        }

        private void CheckMaximumSurface(Coordinates surface)
        {
            if (surface.LatitudeY <= 50 || surface.LongitudeX <= 50)
            {
                Surface = surface;
            }
            else
            {
                Surface = new Coordinates(50, 50);
            }
        }

    }
}
