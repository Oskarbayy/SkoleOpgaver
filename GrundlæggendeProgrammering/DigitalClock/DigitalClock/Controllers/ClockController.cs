using DigitalClock.Models;
using System.Diagnostics;

namespace DigitalClock.Controllers
{
    internal class ClockController
    {
        private Clock _clock;

        public ClockController(Clock clock)
        {
            _clock = clock;
        }
        public void Start()
        {
            Debug.WriteLine(_clock.On());
            while (_clock.On())
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.RightArrow)
                {
                    _clock.ChangeColor(true);
                }
                if (key == ConsoleKey.LeftArrow)
                {
                    _clock.ChangeColor(false);
                }

            }
        }
    }
}
