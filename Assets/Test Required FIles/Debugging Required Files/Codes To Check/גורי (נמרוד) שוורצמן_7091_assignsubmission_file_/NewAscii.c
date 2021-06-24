//מגיש - נמרוד (גורי) שוורצמן ת.ז. 312576614

#include <stdio.h>
int main()
{
	int res1, res2, chardiff;
	char a, b;

	printf("Please Enter Two Chars with only one space between them:");
	res1 = scanf_s("%c ", &a);
	if (res1 < 1)
	{
		printf("Please Enter only Chars!");
		return 1;
	}
	res2 = scanf_s("%c", &b);
	if (res2 < 1)
	{
		printf("Please Enter only Chars!");
		return 1;
	}
	
	chardiff = a - b;
	if (chardiff < 0)
	{
		chardiff = -chardiff;
	}
	printf("Your Char Diffrence is: %d", chardiff);

	return 0;
}
/*
Please Enter Two Chars with only one space between them:7 @
Your Char Diffrence is: 9
C:\Users\97250\Desktop\לימודים\מבוא למדעי המחשב\תרגיל בית 3\ASCII\Debug\ASCII.exe (process 19712) exited with code 0.
To automatically close the console when debugging stops, enable Tools->Options->Debugging->Automatically close the console when debugging stops.
Press any key to close this window . . .

*/