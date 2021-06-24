//תרגיל בית 3 שאלה 2
/*מגד שיהאב
208168690
HM 3.2*/
#include<stdio.h>
int main()
{
	char chr1, chr2;
	int res;
	printf("please enter two letter\n");
	res = scanf_s("%c", &chr1);
	res = scanf_s("%c", &chr2);
	
	if (res > 2)
	{
		printf("eror");

		return 1;

	}

	if (chr1 - chr2 < 0 )
	{
		printf("the difrence is :%d\n", (chr1 - chr2) * -1);
		
	}
	else if (chr2 - chr1 < 0)
	{
		printf("the difrence is %d\n", (chr2 - chr1) * -1);
	}
	return 0;

}
/*please enter two letter
BA
the difrence is 1

C:\Users\windows\Desktop\עבודה מדעי מחשיב\homework.3\x64\Debug\homework.3.exe (process 11816) exited with code 0.
To automatically close the console when debugging stops, enable Tools->Options->Debugging->Automatically close the console when debugging stops.
Press any key to close this window . . .

*/
