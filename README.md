## GE2-Assignment

C16462144 assignment for GE2, a recreation of a space battle using unity

## Idea

For my assignment I am going to try and recreate the Battle for Earth from Halo 2. In the game the 
battle is told from Master Chief's perspective, with glimpses of the battle unfolding in cutscenes. 
I wish to try and recreate a shorter version of the space battle, shown from the perspective of the UNSC
Naval Forces and the perspective of the Covenant. The aim is to have many different camera angles and 
different parts of the battle on display. From the initial forces arriving at earth to them engaging 
Earths defencive forces. Some camera angles I am thinking of include the bridge of a UNSC Frigate, 
Fighters, wider shots of the entire battle and more. Owing to the fact that Halo is widely popular 
3D models can be found of some ships online for free, which should help the end product greatly. Most
of the assets are already imported, but I will be keeping an eye out for extra models should they be
needed.

## Aims

- [x] Find assets
- [x] Steering behaviors on all ships in the scene, bar ships in place for backdrop effect
- [x] Fighter wings engaging big ships in formation
- [x] Object Avoidance
- [ ] Ships falling to earth after being destroyed
- [x] Ships engaging eachother
- [x] Ships exploding after taking enough damage
- [ ] Orbital Defence Platforms engaing enemy ships

## Final Result

In the end, most of what I aimed for was implemented and more. I went outside the coursework and implemented flocking behaviors for the fighters and bombers. I also implemented steering behaviors on all of the ships, gun turrets with firing arcs and also finite state machines on the bigger ships, and the leaders of the fighter and bomber squadrons. I also implemented a camera script that swapped between specific cameras as the story unfolded, and was not specifically timed but triggered by the events unfolding naturally in the scene. I would have liked to implement one more state, for retreating but unfortunately I did not get around to this as the camera and the audio consumed more time than I expected.

