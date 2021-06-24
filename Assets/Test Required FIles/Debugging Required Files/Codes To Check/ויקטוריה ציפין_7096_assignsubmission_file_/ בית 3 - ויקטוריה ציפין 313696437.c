// ויקטוריה ציפין , 313696437


#include<stdio.h>
int main(void)
{
	char a, b, ch;

	printf("enter two char: \n\n\n\n");
	scanf_s("%ch%ch", &a);
	scanf_s("%ch%ch", &b);
	ch = a - b;
	if (ch<0) {


		ch = ch * (-1);
	}
	printf("%d", ch);
	
	return 0;

}

