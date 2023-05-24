# SlotMachine
Wep api solution which mimics slot machine behavior

# Requirements
For the challenge we would like you to build us a slot machine Api.

Build an ASP.NET Core API with MongoDB as the database that will contain two methods.

 

Spin Method:
 

The request will contain the bet the player would like to place.

 

The response will contain the result of the spin in the form of a multi-dimensional integer array where each cell in the array represents a reel in our slot machine, and the content of the cell is the symbol that was selected for that reel.

The response will also contain a field that represents the player win from the specific spin and the current player balance.

 

Definitions:

Result matrix - a multi-dimensional integer array that is randomly populated.

Win line – a path through the result matrix defining the reels that participate in calculating the win amount for a given result matrix.

 

Example:

For a result matrix of 5x3 (Width 5 height 3) some possible winlines are visually represented as follows:

<image.png>

For each spin request the following should occur:

·  The bet should be deducted from the requesting player’s balance

 

·  The result array of the slot machine should be randomly selected as a single digit integer (0-9) for each array cell. the length and height of the array (size of the slot machine) is configurable, and the configuration value is stored in the database.

 

·  The win should be calculated as the game bet multiplied by the sum of consecutive identical digits (where series length >=2) starting from position zero on a specific win line. for example, 3,3,3,4,5 = 9 | 2,3,2 = 0 | 7,7,5,3,1,2,3 = 14.

There can be multiple win lines configured at the same time and the total win for a round will be defined as the sum of wins for each particular line.



·  The game designer has requested you to implement the following win lines, regardless of matrix dimensions:

            1.    Straight across each row (as in sample line 1-3 above)

            2.    Starting from the left column in each row, go diagonally down until you reach the bottom row and then diagonally up until you reach the top row and repeat (as in sample lines 4-5 above, but keep in mind these are examples and not necessarily the only possible lines)


For example, for the following 5 x 3 result matrix:

3 3 3 4 5

2 3 2 3 3

1 2 3 3 3

The total win would be calculated as:

[0,0] + [0,1] + [0,2] (top row across, i.e. sample line 2) = (3+3+3) * bet +

[0,0] + [1,1] + [2,2] + [1,3] (diagonally from top row) = (3+3+3+3) * bet +

[1,1] + [2,2] + [1,3] (diagonally from 2nd row) = (2+2+2) * bet

for a total of 27 * bet

 

·   The Win should be added to the player balance.

 

Update Balance Method:

·   The request will contain the amount to be added to the player balance.

·   The amount will be added to the player balance and committed to DB

General Emphasis for the API:

· Update Balance and spin calls can be called simultaneously 


· Matrix size should be re-configurable without restarting the application.