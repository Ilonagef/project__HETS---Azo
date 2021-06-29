#include <stdio.h> //תמיר מירונוב 206010282
int main()
{
	char a, b, c;
	int diff;
	printf("Please enter two character with space:\n");
	scanf_s("%c", &a);
	scanf_s("%c", &b);
	scanf_s("%c", &c);

	if (b != ' ')
	{
		printf("Error: there is no space between two characters\n");
		return 0;
	}

	diff = a - c;

	if (diff < 0)
	{
		diff = -1 * diff;
	}
	printf("The diff between two character is: %d", diff);

	return 0;
}

/*
Please enter two character with space:
c9
Error: there is no space between two characters

Please enter two character with space:
@ 7
The diff between two character is: 9

Please enter two character with space:
7 u
The diff between two character is: 62

*/