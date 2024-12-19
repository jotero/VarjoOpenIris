// Copyright 2022 Varjo Technologies Oy. All rights reserved.

#pragma once

#include <Varjo_types_datastream.h>

struct FrameInfo {
    long long frameIndex;  // Frame index
    long long timestamp;   // Frame timestamp
    long long channelIndex;
};

typedef bool(*CallbackType)(uint8_t* frameData, int size, FrameInfo frameInfo);

class IApplication
{
public:
    struct Options {
        varjo_ChannelFlag channels;
    };

    virtual ~IApplication() = default;

    virtual void run() = 0;
    virtual void terminate() = 0;

    CallbackType openIris_callback = nullptr;
};
