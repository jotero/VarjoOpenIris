#include "OpenIrisInterface.h"


// Application instance
std::unique_ptr<IApplication> g_application;

// Function to free the allocated frame data
int FreeFrameData(uint8_t* frameData) {
    if (frameData) {
        free(frameData);
    }
    return 0;
}

int VarjoStartCameras(CallbackType callback) {

    IApplication::Options appOptions;
    appOptions.channels = varjo_ChannelFlag_First | varjo_ChannelFlag_Second; // Left is first, right is second

    try {
        // Initialize session
        auto session = std::make_shared<Session>();
        if (!session->isValid()) {
            throw std::runtime_error("Failed to initialize session. Is Varjo system running?");
        }

        // Initialize application
        g_application = std::make_unique<StreamingApplication>(std::move(session), appOptions);

        // Attach the open iris callback
        g_application->openIris_callback = callback;

        // Execute application
        g_application->run();

        // Application finished
        return EXIT_SUCCESS;
    }
    catch (const std::exception& e) {
        std::cerr << "Critical error caught: " << e.what();
        return EXIT_FAILURE;
    }
}

int VarjoStop() {
    g_application->terminate();
    return 0;
}

