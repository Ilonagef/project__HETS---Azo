/*    תרגיל בית 3 שאלה 2
ליית גאנם 212142681
בהאא מקלדה 207287681
*/
#include<stdio.h>

int main()
{
	char x, y;
	int diff,res1,res2;

	printf("please enter a characters\n");
	res1=scanf_s("%c", &x);

	if (res1 < 1)
	{
		printf("you must enter a character ");
		return 1;
	}

	printf("please enter a secend character ");
	res2=scanf_s("%c", &y);

	if (res2 < 1)
	{
		printf("you must enter a character ");
		return 1;
	}

	diff = x - y;
	if (diff < 0)
	{
		diff = -1 * diff;
	}

	printf("the absolute value of characters is : %d\n", diff);

	system("PAUSE");
	return 0;



}