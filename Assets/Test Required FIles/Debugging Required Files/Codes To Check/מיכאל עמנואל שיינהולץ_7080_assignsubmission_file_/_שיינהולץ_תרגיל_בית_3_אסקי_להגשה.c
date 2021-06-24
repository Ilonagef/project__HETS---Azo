
/******************************************************************************
//Homework Assignment 3 Exercise 2 for Prof. Malki Grossman, TA Waseem Makhoul
//Mikhael Scheinholtz, T.Z. 337-869-259
*******************************************************************************/

/**********Version 1 Debugged, Built, and Compiled **********/

#include <stdio.h>

int
main()
{

	char chr1, chr2, space_sep;
	int diff;

	printf("Please enter two single characters, separated by a space. \n");
	//	scanf_s("%c%c%c", &chr1, &space_sep, &chr2); (will result in error)
	scanf_s("%c", &chr1);
	scanf_s("%c", &space_sep);
	scanf_s("%c", &chr2);

	if ((space_sep == ' ') && (chr1 != 0 && chr1 != 10)
		&& (chr2 != 0 && chr2 != 10))
	{
		diff = chr1 - chr2;
		if (diff < 0)
		{
			diff = -1 * diff;
		}
		else
		{
			diff = diff;
		}

		printf("You entered the characters %c and %c.", chr1, chr2);
		printf
		("The ASCII values of the characters are %d and %d, respectively.",
			chr1, chr2);
		printf
		("The absolute value of the difference between the ASCII values of each character you typed is %d",
			diff);
	}
	else
	{
		printf("Input Error! Please restart the program\n");
		printf("and retype the input characters making sure\n");
		printf("they are within the right criteria.");
	}

	return 0;
}

//Please enter two single characters, separated by a space.
//5 5
//You entered the characters 5 and 5.The ASCII values of the characters are 53 and 53, respectively.The absolute value of the difference between the ASCII values of each character you typed is 0
//D:\Documents\Visual Studio 2019\source\repos\in class 21 11\Debug\in class 21 11.exe(process 2552) exited with code 0.
//	To automatically close the console when debugging stops, enable Tools->Options->Debugging->Automatically close the console when debugging stops.
//	Press any key to close this window . . .




/**********Version 2 (Needs to be tuned to build and compile properly)**********/



/*
#include <stdio.h>

int
main ()
{

  char chr1, chr2, space_sep;
  int diff;

  printf ("Please enter two single characters, separated by a space. \n");
  scanf ("%c%c%c", &chr1, &space_sep, &chr2);

  if ((space_sep == ' ') && (chr1 != 0 && chr1 != 10)
	  && (chr2 != 0 && chr2 != 10))
	{
	  diff = chr1 - chr2;
	  if (diff < 0)
	{
	  diff = -1 * diff;
	}
	  else
	{
	  diff = diff;
	}

	  printf ("You entered the characters %c and %c.", chr1, chr2);
	  printf
	("The ASCII values of the characters are %d and %d, respectively.",
	 chr1, chr2);
	  printf
	("The absolute value of the difference between the ASCII values of each character you typed is %d",
	 diff);
	}
  else
	{
	  printf ("Input Error! Please restart the program\n");
	  printf ("and retype the input characters making sure\n");
	  printf ("they are within the right criteria.");
	}

  return 0;

}

*/

//Please enter two single characters, separated by a space.                                                               
//i j                                                                                                                     
//You entered the characters i and j.The ASCII values of the characters are 105 and 106, respectively.The absolute value o
//f the difference between the ASCII values of each character you typed is 1                                              

//...Program finished with exit code 0                                                                                    
//Press ENTER to exit console.  


