
#pragma once

#include "Session.hpp"
#include "StreamingApplication.hpp"
#include "UIApplication.hpp"
#include <cxxopts.hpp>



#ifdef VARJOOPENIRIS_EXPORTS
#define VARJOOPENIRIS_API __declspec(dllexport)
#else
#define VARJOOPENIRIS_API __declspec(dllimport)
#endif




#ifdef __cplusplus
extern "C" {
#endif

	VARJOOPENIRIS_API int GetLastFrame(uint8_t** frameData, int* size, FrameInfo* frameInfo);
	VARJOOPENIRIS_API int FreeFrameData(uint8_t* frameData);
	VARJOOPENIRIS_API int VarjoStartCameras(CallbackType callback);
	VARJOOPENIRIS_API int MyFunction2();

#ifdef __cplusplus
}
#endif