
/*איימן שפסו ת.ז 305782351*/
#include <stdio.h>
int main()
{
	char x, z, y;

	printf("pls Enter two letters () - ():");
	scanf_s("%c", &x, 1);//(אני חייב להוסיך את ה 1 אחרת זה לא עובד)
	scanf_s("%c", &z, 1);// (אני חייב להוסיך את ה 1 אחרת זה לא עובד)
	scanf_s("%c", &y, 1);// (אני חייב להוסיך את ה 1 אחרת זה לא עובד)

	if (z != ' ')
	{
		printf("input error");
		return 1;
	}
	int diff = x - y;
	if (diff < 0)
		diff = -1 * diff;
	printf("the diff betwen two letters is : %d\n", diff);
	return 0;
}
/*זה ללא תנאי לתקלה*/
/*pls Enter two letters () - ():@ u
the diff betwen two letters is : 53

C:\Users\Ayman\source\repos\Project15\Debug\Project15.exe (process 68528) exited
 with code 0.
To automatically close the console when debugging stops, enable Tools->Options->
Debugging->Automatically close the console when debugging stops.
Press any key to close this window . . .
*/

/*זה עם התנאי לתקלה*/
/*pls Enter two letters () - ():ab
input error
C:\Users\Ayman\source\repos\Project15\Debug\Project15.exe (process 52860) exited
 with code 1.
To automatically close the console when debugging stops, enable Tools->Options->
Debugging->Automatically close the console when debugging stops.
Press any key to close this window . . .
*/