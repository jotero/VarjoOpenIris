# Copyright 2022 Varjo Technologies Oy. All rights reserved.

# Project name
set(_app_name "VarjoOpenIrisTestApp")
project(${_app_name})

# Runtime output directories
set(_build_output_dir ${CMAKE_BINARY_DIR}/bin)
foreach(OUTPUTCONFIG ${CMAKE_CONFIGURATION_TYPES})
    string(TOUPPER ${OUTPUTCONFIG} OUTPUTCONFIG)
    set(CMAKE_RUNTIME_OUTPUT_DIRECTORY_${OUTPUTCONFIG} ${_build_output_dir})
endforeach(OUTPUTCONFIG CMAKE_CONFIGURATION_TYPES)

# Application sources
set(_sources_app
     ${CMAKE_CURRENT_SOURCE_DIR}/main.cpp
)

# Visual studio source groups
source_group("Application" FILES ${_sources_app})

# Application exe target
set(_target ${_app_name})
add_executable(${_target}
    ${_sources_app}
)
target_link_libraries(${_target}
    PRIVATE VarjoOpenIrisLib
)

# Include directories
target_include_directories(${_target} PRIVATE ${CMAKE_CURRENT_SOURCE_DIR}/VarjoOpenIrisLib)

# VS properties
set_target_properties(${_target} PROPERTIES VS_DEBUGGER_WORKING_DIRECTORY "${CMAKE_BINARY_DIR}/bin")
#set_property(TARGET ${_target} PROPERTY FOLDER "Examples")

# Compile and link as console application
set_target_properties(${_target} PROPERTIES LINK_FLAGS /SUBSYSTEM:CONSOLE)

# Preprocerssor definitions
target_compile_definitions(${_target} PUBLIC -D_UNICODE -DUNICODE)
