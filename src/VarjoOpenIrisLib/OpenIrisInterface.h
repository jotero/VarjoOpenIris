/*#pragma once

// When building the DLL, define MYLIBRARY_EXPORTS (e.g., in project settings).
#ifdef MYLIBRARY_EXPORTS
#define MYLIBRARY_API __declspec(dllexport)
#else
#define MYLIBRARY_API __declspec(dllimport)
#endif

#ifdef __cplusplus
extern "C" {
#endif

	// Declare the function(s) you want to export
	MYLIBRARY_API int MyFunction(int a, int b);

#ifdef __cplusplus
}
#endif


*/

#pragma once

#ifdef VARJOOPENIRIS_EXPORTS
#define VARJOOPENIRIS_API __declspec(dllexport)
#else
#define VARJOOPENIRIS_API __declspec(dllimport)
#endif

#ifdef __cplusplus
extern "C" {
#endif

	VARJOOPENIRIS_API int MyFunction(int argc, char** argv);
	VARJOOPENIRIS_API int MyFunction2();

#ifdef __cplusplus
}
#endif