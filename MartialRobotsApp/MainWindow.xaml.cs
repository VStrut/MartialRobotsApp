using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MartialRobotsApp
{
    public partial class MainWindow : Window
    {
        private string InputText { get; set; } = string.Empty;
        private bool CanDeployRobot { get; set; } = false;
        private bool RobotsMoved { get; set; } = false;
        private List<Robot> Robots = new List<Robot>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CheckIfCommandsArePresent();
            GetTextFromControlInput();
            DeployRobots();
            PrintOutput();
        }

        private void DeployRobots()
        {
            if (CanDeployRobot)
            {
                CommandsHelper();
            }
        }

        private void GetTextFromControlInput()
        {
            InputText = txtBoxInput.Text.ToString();
        }

        private void CheckIfCommandsArePresent()
        {
            GetTextFromControlInput();

            var inputLength = InputText.Length;

            if (inputLength > 0)
            {
                ClearOutputControl();
                CanDeployRobot = true;
            }
            else
            {
                txtBlockOutput.Text = "Introduce a valid command/s";
            }
        }

        private void ClearOutputControl()
        {
            txtBlockOutput.Text = String.Empty;
        }

        private void CommandsHelper()
        {
            GetTextFromControlInput();

            var splittedCommands = InputText.Split('\n').ToList();
            var surfaceSplitted = splittedCommands[0].Split(' ');

            // Get Surface
            var latitudeX = int.Parse(surfaceSplitted[0]);
            var longitudeY = int.Parse(surfaceSplitted[1]);

            Coordinates surface = new(latitudeX, longitudeY);

            splittedCommands.RemoveAt(0);

            for (int i = 0; i < splittedCommands.Count; i++)
            {
                var robotPositions = splittedCommands[i].Split(' ');
                var robotPositionX = int.Parse(robotPositions[0]);
                var robotPositionY = int.Parse(robotPositions[1]);
                var robotOrientationString = robotPositions[2].Trim();
                var robotOrientation = HelperGetOrientationByLetter(robotOrientationString);

                Robot robot = new(new Coordinates(robotPositionX, robotPositionY), robotOrientation, surface);
                robot.Move(splittedCommands[i + 1]);
                Robots.Add(robot);
                i++;
            }

            if (Robots.Count > 0)
            {
                RobotsMoved = true;
            }
        }

        private static Robot.Orientations HelperGetOrientationByLetter(string orientationString)
        {
            if (orientationString == "N")
            {
                return Robot.Orientations.North;
            }
            else if (orientationString == "E")
            {
                return Robot.Orientations.East;
            }
            else if (orientationString == "S")
            {
                return Robot.Orientations.South;
            }
            else
            {
                return Robot.Orientations.West;
            }
        }

        private void PrintOutput()
        {
            StringBuilder sbOutput = new StringBuilder();

            if (RobotsMoved)
            {
                foreach (var robot in Robots)
                {
                    var robotLongitudeX = robot.Coordinate.LongitudeX.ToString() + " ";
                    var robotLatitudeY = robot.Coordinate.LatitudeY.ToString() + " ";
                    var robotOrientation = robot.Orientation.ToString().Substring(0, 1) + " ";

                    sbOutput.AppendLine(robotLongitudeX + robotLatitudeY + robotOrientation + robot.LostText);
                }
                
                txtBlockOutput.Text = sbOutput.ToString();
            }
        }
    }
}