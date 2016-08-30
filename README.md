# AirTrafficControl

"AirTrafficControl" is a simulation, simulating the air traffic (captured by radars). :eyes:

# Overengineering

Thirst of all I had to think pretty hard about the overengineering task, because I didn't know which part I should overengineer.
So after a bunch of thinking and brainstorming I got to monogame and shaders! This simulation should've stand on three shaders:

* Grid shader (which draws a grid -> for a older stylish look)
* RGB-Rumble shader
* Noise shader (which should generate the background)

Because of a bug of monogame (which I'm not sure it is a bug, for further information look up to the [Bugreproduction](https://github.com/TheRealVira/BugReproduction))
, I cannot get my noise running, which is really sad. :cry:

# What it is simulating

This programm is simulating airplanes (with cool namens and nice textures :stuck_out_tongue_winking_eye:) flying across the field of radars and airports.
Every airport has it's own radar, which has a random radius and position and the airport is signed as a green dot in the middle.
If a airplane has been seen anytime (via a airport radar or the "big-all-over"-radar) it will be added to the "Seen"-list, noted on
the left site. If the airplane has landed, it will than be removed from the "Seen"-list and added to the "Landed"-list, which is noted on
the right site. The "big-all-over"-radar is a nice fading green stripe, which moves from the left site to the left site.

# Overall thoughts
It's a decent project and a need challenge, which has a bunch of potential, if you're getting into it.
If I had further time, I'd implement two external .txt files, which should contain all names for the airplanes/airports, so everybody
could easily add custom names. Also I might implement multiple airplane textures, for more texture fun! :neckbeard:
  
No further writing here and as always:

Happy coding! :necktie:
