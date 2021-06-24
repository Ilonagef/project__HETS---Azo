#include <stdio.h>אלמוג ברום 315688325
int main()
{
	char a,b,c;
	int diff;
	printf("Please Enter 2 numbers!\n");
	 scanf_s("%c%c%c", &a, &b, &c);
	 diff = a - b;
	 
	if (diff < 0)
	{
		diff = -1 * diff;

	}
	printf("The diff between the 2 number is %d", diff);
	if(c=' ')
	{
		printf("Error");
	}
	

	
	return 0;


}