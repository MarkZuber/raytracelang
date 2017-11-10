cmake_minimum_required (VERSION 3.9.0)
project (CppRayTrace)

FILE(GLOB AllCpp "*.cpp")
add_executable(CppRayTrace ${AllCpp})