/*ענבל מיכאלי 205923113 עבודה להגשה מספר 3*/
#include <stdio.h>
int main ()
{
	char ch1, ch2;

	printf("Enter two character: ");
	scanf_s("%c\n", &ch1);
	scanf_s("%c", &ch2);

	int diff = ch1 - ch2;
	int abs_diff=diff*(-1);

	if (diff<0){

		printf("The diff of the characters is: %d", abs_diff);
	}

	else
	{
		printf("The diff of the characters is: %d", diff);
	}
	return(0);
}

/*Enter two character : 7 @
The diff of the characters is : 9
C : \Users\micha\source\repos\homework3\Debug\homework3.exe(process 3476) exited with code 0.
To automatically close the console when debugging stops, enable Tools->Options->Debugging->Automatically close the console when debugging stops.
Press any key to close this window . . .
*/