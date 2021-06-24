//תרגיל בית 3
//ואסים חורי 206944902
#include <stdio.h>
 
int main() {

	char ch1, ch2, ch3;
	

	printf("may you enter charecters ((charecter)(space)(charecter)) :");

    scanf_s ("%c", &ch1);
	scanf_s ("%c", &ch2);
	scanf_s ("%c", &ch3);

	if (ch2 != ' ') {

		printf("input error!!");
		return 0;
}
    else{

          printf("the difference is: %d",abs (ch1 - ch3));
         }
		

       return 0;
	}

/* אם יש רווח (קלט חוקי
may you enter charecters ((charecter)(space)(charecter)) :a b
the difference is: 1
C:\Users\Wassim Khoury\source\repos\homework_01_q2\Debug\homework_01_q2.exe (process 15268) exited with code 0.
To automatically close the console when debugging stops, enable Tools->Options->Debugging->Automatically close the console when debugging stops.
Press any key to close this window . . .

*/
	 
/* אם אין רווח (קלט לא חוקי 
may you enter charecter:ab
input error!!
C:\Users\Wassim Khoury\source\repos\homework_01_q2\Debug\homework_01_q2.exe (process 2440) exited with code 0.
To automatically close the console when debugging stops, enable Tools->Options->Debugging->Automatically close the console when debugging stops.
Press any key to close this window . . .

*/

