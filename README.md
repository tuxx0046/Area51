# Area51
A small challenge with the goal of learning the basics of concurrency in programming with C#, and should also train the object oriented thought process.
Project is estimated to take 10 days to solve. 

The project description is in danish, but to summarize, Area 51 has faced some problems with security. 
Intruders have been using the elevator without problem, and now it is the programmers job to implement a system that allows personnel with clearance to use the elevator, and for intruders to be shot by turrets installed on each floor.

## Requirements:
**Create a loop that spawns 20 people randomly.**
*Spawning should happen independent(asynchronously) from Elevator movement**  

**Solution should be object oriented.**

**Floors**
Number of floors are optional, but each floor should have:
*a scanner*
*a Turret*
*a panel* (to call the elevator)

**Panel**
*Should be able to request the elevator*

**Scanner**
*Should be able to read the security certificate of personnel*

**Turret**
*Should be able to eliminate personnel with insufficient security clearance*
*Should be able to send kill confirmed status*

**Control**
*Should be able to receive elevator requests*
*Should be able to receive security information from scanner*
*Should be able to send security information*
*Should be able to send order to Turret*
*Should be able to receive kill confirmed status*
*Should be able to accept elevator requests to the Elevator queue*

**Elevator**
*Must be able to travel floors and travel should take time*
*Should have a target floor*
*Can only carry 1 person at a time*
*Should have a functional "first in first out" list of orders (request queue)*
*Should have a floor panel*

**Floor Panel**
*Should be able to receive security information from Control
Should be able to stop the user from going to floors without the correct security certificate
Should be able to send Target Floor to Elevator*

**Personnel**
*Should have security certificates from 0 to 5, where 0 indicates uncertified intruder
Randomized certificate
Randomized spawn floor
Randomized target floor
Randomized time of spawning
Must be able to die*

Tip:
Main function could be used to spawn personnel in a while loop.

