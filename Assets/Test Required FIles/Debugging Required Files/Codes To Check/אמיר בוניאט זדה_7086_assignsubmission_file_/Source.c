#include <stdio.h> //אלמוג אבירם אמיר זדה

int main()
{
	char x, space, y;
	int diff;
	printf("enter 2 chars seperated by space:\n");
	scanf_s("%c",&x);
	scanf_s("%c", &space);
	scanf_s("%c", &y);
	if (space != ' ') {
		printf("The chars must be seperated by a comma!\n");
	}
	else {
		diff = x - y;
		if (diff < 0) {
			diff = 0 - diff;
		}
		printf("your char diff is: %d\n", diff);
	}
}
//enter 2 chars seperated by space:
//A B
//your char diff is : 1
//C : \Users\אלמוג פאקינג אבירם\source\repos\Project3\Debug\Project3.exe(process 23424) exited with code 0.
//	To automatically close the console when debugging stops, enable Tools->Options->Debugging->Automatically close the console when debugging stops.
//	Press any key to close this window . . .