/*Andreina Olivares I.D. 338014509*/
#include<stdio.h>
int main()
{
	unsigned char a, b, c;
	unsigned char ch3;

	printf("Please enter two characters:\n");
	scanf_s("%c", &a);
	scanf_s("%c", &c);
	scanf_s("%c", &b);
	
	if (c != ' ')
	{
		printf("Input Error");
		return 1;
	}
	else
	if (a < b)
	{
		ch3 = b - a;
	}
	else
	{
		ch3 = a - b;
	}

	printf("The difference of the values of %c and %c in ASCII is: %d", a,b, ch3);

	return 0;
}

/*WITH SPACE BETWEEN THE CHARACTERS*/
/*
Please enter two characters:
K J
The difference of the values of K and J in ASCII is: 1
C:\Users\oliva\source\repos\תרגיל בית 3 שאלה 2\Debug\תרגיל בית 3 שאלה 2.exe (process 18640) exited with code 0.
Press any key to close this window . . .
*/

/*WITHOUT SPACE BETWEEN THE CHARACTERS*/
/*
Please enter two characters:
KJ
Input Error
C:\Users\oliva\source\repos\תרגיל בית 3 שאלה 2\Debug\תרגיל בית 3 שאלה 2.exe (process 5036) exited with code 1.
Press any key to close this window . . .

*/