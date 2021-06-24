// מגיש: שלומי שאול 207494360
#include <stdio.h>
int main()
{
	char x, y;
	int z, res;

	printf("please enter two letters\n");
	res = scanf_s("%c ", &x);
	if (res < 1)
	{
		printf("Error: Please Enter a char");
		return 1;
	}
	res = scanf_s("%c", &y);
	if (res < 1)
	{
		printf("Erroe: Please Enter a char");
		return 1;
	}

	if (x >= y)
	{
		z = x - y;
	}
	if (y >= x)
	{
		z = y - x;
	}

	printf("The Diff is: %d", z);
	
	return 0;
}

/*
please enter two letters
7 @
The Diff is: 9
C:\Users\819\Desktop\Project shlomi\Debug\Project shlomi.exe (process 10436) exited with code 0.
To automatically close the console when debugging stops, enable Tools->Options->Debugging->Automatically close the console when debugging stops.
Press any key to close this window . . .

*/