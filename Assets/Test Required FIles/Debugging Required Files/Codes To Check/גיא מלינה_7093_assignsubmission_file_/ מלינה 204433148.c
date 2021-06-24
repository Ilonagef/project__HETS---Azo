
//גיא מלינה 204433148

#include<stdio.h>
int main(VOID)
{
	char a, b, ch;
	printf("Enter two character: \n\n");

	scanf_s("%ch%ch", &a);
	scanf_s("%ch%ch", &b);

	ch = a - b;

	if (ch < 0)
	{
		ch = ch * (-1);
	}

	printf("The difference of the two character in Ascii is: %d\n\n\n\n", ch);

	printf("The value of the first number in Ascii is: %d\n\nThe value of second number in Ascii is: %d\n\n\n", a, b);

	return 0;

}
