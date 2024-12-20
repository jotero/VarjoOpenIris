# Copyright 2022 Varjo Technologies Oy. All rights reserved.

# Project name
set(_app_name "VarjoOpenIrisLib")
project(${_app_name})

# Runtime output directories
set(_build_output_dir ${CMAKE_BINARY_DIR}/bin)
foreach(OUTPUTCONFIG ${CMAKE_CONFIGURATION_TYPES})
    string(TOUPPER ${OUTPUTCONFIG} OUTPUTCONFIG)
    set(CMAKE_RUNTIME_OUTPUT_DIRECTORY_${OUTPUTCONFIG} ${_build_output_dir})
endforeach(OUTPUTCONFIG CMAKE_CONFIGURATION_TYPES)

# Application sources
set(_sources_app
    ${CMAKE_CURRENT_SOURCE_DIR}/DataStreamer.hpp
    ${CMAKE_CURRENT_SOURCE_DIR}/DataStreamer.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/Globals.hpp
    ${CMAKE_CURRENT_SOURCE_DIR}/Globals.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/Session.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/Session.hpp
    ${CMAKE_CURRENT_SOURCE_DIR}/Undistorter.hpp
    ${CMAKE_CURRENT_SOURCE_DIR}/Undistorter.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/EyeCameraStream.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/EyeCameraStream.hpp
    ${CMAKE_CURRENT_SOURCE_DIR}/FPSCalculator.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/FPSCalculator.hpp
    ${CMAKE_CURRENT_SOURCE_DIR}/IApplication.hpp
    ${CMAKE_CURRENT_SOURCE_DIR}/StreamingApplication.cpp
    ${CMAKE_CURRENT_SOURCE_DIR}/StreamingApplication.hpp
    ${CMAKE_CURRENT_SOURCE_DIR}/OpenIrisInterface.h
    ${CMAKE_CURRENT_SOURCE_DIR}/OpenIrisInterface.cpp
)

# Visual studio source groups
source_group("Application" FILES ${_sources_app})

# Application exe target
set(_target ${_app_name})
add_library(${_target} SHARED 
    ${_sources_app}
)

# Include directories
target_include_directories(${_target} PUBLIC 
    ${CMAKE_CURRENT_SOURCE_DIR})

# Linked libraries
target_link_libraries(${_target}
    PRIVATE VarjoLib
    PRIVATE CxxOpts
    PRIVATE d3d11
    PRIVATE dxgi
    PRIVATE d3dcompiler
    PRIVATE GLM::GLM
    PRIVATE ImGui::ImGui
    PRIVATE ImGui::ImGuiBackends
    PRIVATE FreeType::FreeType
)

# VS properties
set_target_properties(${_target} PROPERTIES VS_DEBUGGER_WORKING_DIRECTORY "${CMAKE_BINARY_DIR}/bin")

# Compile and link as console application
set_target_properties(${_target} PROPERTIES LINK_FLAGS /SUBSYSTEM:CONSOLE)

# Preprocerssor definitions
target_compile_definitions(${_target} PUBLIC -D_UNICODE -DUNICODE)

# Define a preprocessor macro so that in code we know we are building the DLL
target_compile_definitions(${_target}  PRIVATE VARJOOPENIRIS_EXPORTS)

# Link against any system libraries if needed (e.g., Winsock for networking)
if(WIN32)
    target_link_libraries(VarjoOpenIrisLib PRIVATE ws2_32)
endif()