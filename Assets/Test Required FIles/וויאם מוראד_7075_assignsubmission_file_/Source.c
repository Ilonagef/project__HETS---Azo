 /*וויאם מוראד ת'ז 316510106
 אפריים יוסופוב ת'ז 307990879*/
#include<stdio.h> 
int main()
{
	char x, y, z;
	int diff;
	printf("Please enter two character with space:\n");
	scanf_s("%c", &x);
	scanf_s("%c", &y);
	scanf_s("%c", &z);

	if (y != ' ')
	{
		printf("Error: there is no space between two characters\n");
		return 1;
	}
	
	diff = x - z;

	if (diff < 0)
	{
		diff = -1 * diff;
	}
	printf("The diff between two character is: %d", diff);

	return 0;
}
