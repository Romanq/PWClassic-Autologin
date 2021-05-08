#pragma once
#include "Dialog.h"
#include <winternl.h>

typedef UIObject*(__thiscall* oGetDialog)(void* ecx, char* dialogname);
typedef DWORD (__thiscall* oGetWindow)(void* ecx, char* dialogname);
typedef DWORD(__thiscall* oShowWindow)(void*, int, int, int);


template<typename T, typename P>
T remove_if(T beg, T end, P pred)
{
    T dest = beg;
    for (T itr = beg; itr != end; ++itr)
        if (!pred(*itr))
            *(dest++) = *itr;
    return dest;
}
