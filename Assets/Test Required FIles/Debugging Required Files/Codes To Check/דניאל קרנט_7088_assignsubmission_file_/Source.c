/*דניאל קרנט
203980867
עבודת בית 3 קובץ 1 שאלה 2*/


#include<stdio.h>

int main()

{

	char ch1, ch2;
	int diff;
	printf("Please enter two characters\n");
	scanf_s("%c", &ch1);
	scanf_s("%c", &ch2);

	diff = ch1 - ch2;

	if (diff < 0)
		diff = -1 * diff;

	printf("The absolute value of characters is: %d", diff);

	return 0;
}

/*Please enter two characters
AB
The absolute value of characters is: 1
C:\Users\user\source\repos\Project5\Debug\Project5.exe (process 1408) exited with code 0.
To automatically close the console when debugging stops, enable Tools->Options->Debugging->Automatically close the console when debugging stops.
Press any key to close this window . . .
*/