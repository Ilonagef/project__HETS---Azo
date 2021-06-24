#include <stdio.h>
int main()
{
    char x,y,T;
    int f=0,z=0,diff;
    
    printf("prees two letter\n");
    scanf("%c%c%c",&x,&T,&y);
    
        z=x;
        f=y;
       
    
     if (T==' ')
    {
        diff=z-f;
           
             if (diff<0) {
                      diff=diff*-1;
                         }
           else
                      diff=diff;
             
    printf("the diff is %d\n",diff);
   }
     else {
        printf("error\n");
        
    }
       
    
    
    
return 0;
}
