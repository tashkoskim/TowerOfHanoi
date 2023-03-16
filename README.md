
# Tower of Hanoi

## Tower of Hanoi console game
The **Tower of Hanoi** (also called **The problem of Benares Temple** or **Tower of Brahma** or **Lucas' Tower** and sometimes pluralized as **Towers**, or simply **pyramid puzzle**) is a mathematical game or puzzle consisting of three rods and a number of disks of various diameters, which can slide onto any rod. The puzzle begins with the disks stacked on one rod in order of decreasing size, the smallest at the top, thus approximating a conical shape.  

With 3 disks, the puzzle can be solved in 7 moves. The minimal number of moves required to solve a Tower of Hanoi puzzle is 2^n − 1, where n is the number of disks. You can read more on [wikipedia](https://en.wikipedia.org/wiki/Tower_of_Hanoi). 

## Rules
The objective of the puzzle is to move the entire stack to the last rod, obeying the following rules:
- Only one disk may be moved at a time.
- Each move consists of taking the upper disk from one of the stacks and placing it on top of another stack or on an empty rod.
- No disk may be placed on top of a disk that is smaller than it. 

## The application
The application was developed in *.NET Framework 6.0* (C#) as a *console application*. At the begining the user must write the number of disks. Then the towers first state is drawn with ASCII characters. In order to use [extended ASCII characters](https://theasciicode.com.ar/), at the begining of my console application I have changed the encoding `Console.OutputEncoding = System.Text.Encoding.UTF8`. Then another menu will show where you can choose playing the game or the game to be played automatically. I used also text files for generating the logs (all of the moves made during the game) at the end of the game. There are 2 possible log files *dd_M_yyyy_MovesLog_User.txt* and *dd_M_yyyy_MovesLog_Computer.txt*. The header is generated with this text to ASCII generator [website](https://patorjk.com/software/taag/#p=display&f=Calvin%20S&t=Tower%20of%20Hanoi) and the used font is called "Calvin S".

## Screenshots
Screenshot of the sub-menu, when the user should choose who will play the game (towers with 7 disks):  
![SubMenu](https://github.com/tashkoskim/TowerOfHanoi/blob/master/HanoiTower/ScreenShots/HanoiTower_SubMenu.png?raw=true)   

Screenshot of the user before making the first move (towers with 7 disks):  
![UserPlay](https://github.com/tashkoskim/TowerOfHanoi/blob/master/HanoiTower/ScreenShots/HanoiTower_UserPlay.JPG?raw=true)  

Screenshot of the log file (with some errors) for towers with 3 disks:  
![Log](https://github.com/tashkoskim/TowerOfHanoi/blob/master/HanoiTower/ScreenShots/HanoiTower_Log.png?raw=true)  

### GIF demo of the game
The demo is how the computer play the game for towers with 4 disks:  
![ComputerPlay](https://github.com/tashkoskim/TowerOfHanoi/blob/master/HanoiTower/ScreenShots/HanoiTower_ComputerPlay.gif?raw=true) 


## Authors
- tashkoskim@yahoo.com


