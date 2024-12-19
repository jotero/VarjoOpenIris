// Copyright 2022 Varjo Technologies Oy. All rights reserved.

/* Eye Tracking Camera Stream Example application
 *
 * - Showcases how eye tracking camera stream can be retrieved using Varjo data stream API
 */

 // FOR DLL loading dynamically
//typedef int (*MYFUNC)(int, int); // Match the function signature you want to call
// END FOR DLL

#include "OpenIrisInterface.h" // This includes the __declspec(dllimport) functions


#include <iostream>


// Application entry point
int main(int argc, char** argv)
{
    int test = MyFunction2(); 

    int result = VarjoStartCameras(nullptr);
    std::cout << "The result of MyFunction(2, 3) is: " << result << std::endl;

}
