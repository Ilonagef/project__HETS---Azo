//לואי נסר אל דין 322828724
//hw01תרגיל בית 3 שאלה 2 מקובץ



#include<stdio.h>
int main()
{
	char ch1, ch2;
	int diff;
	printf("PLEASE ENTER TWO CHARACTERS:");
	scanf_s("%c", &ch1);
	scanf_s("%c", &ch2);
	diff = ch1 - ch2;
	if (diff < 0)
		diff = -1 * diff;
	printf("THE ABSOLUTE VALUE IS:%d", diff);
	return 0;

}
/*PLEASE ENTER TWO CHARACTERS:k#
THE ABSOLUTE VALUE IS:72
C:\Users\PC-Kissra\Desktop\מדעי המחשב\3\HW3\Debug\HW3.exe (process 13472) exited with code 0.
Press any key to close this window . . .
*/