#include<stdio.h>   //vladimir drob 312964844
int main()
{

	char a, b, c;
	int diff;
	printf("plese enter two chars with space between them\n");
	scanf_s("%c", &a);
	scanf_s("%c", &b);
	scanf_s("%c", &c);

	

	    if (b !=' ')
	    {
		printf("error! type space between the chars\n");
		return 1;
	    }

	     diff = a - c;

	          if (diff < 0)
	          {
				  diff = -1 * diff;

           	}
			  printf(" the absolute number between two chars is %d", diff);

			  return 0;
}

/*plese enter two chars with space between them
A B
 the absolute number between two chars is 1*/

/*plese enter two chars with space between them
@ 7
 the absolute number between two chars is 9*/

/*plese enter two chars with space between them
Q Q
 the absolute number between two chars is 0*/
/*plese enter two chars with space between them
&^%
error! type space between the chars
*/
