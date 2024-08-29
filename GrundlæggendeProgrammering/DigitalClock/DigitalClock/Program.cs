using DigitalClock.Models;
using DigitalClock.Controllers;

Console.CursorVisible = false;



Clock clock = new Clock();
Thread thread = new Thread(() =>  
{ 
    clock.Start(); 
});
thread.Start();

ClockController controller = new ClockController(clock);
controller.Start();