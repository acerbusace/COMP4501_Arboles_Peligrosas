/*
    void OnCollisionEnter(Collision col)
    {
        if (status != Status.RUNNING)
        {
            stop();
            return;
        }

        transform.position = lastPosition;

        //if (col.gameObject.tag != "Ground")
        if (!HelperFunctions.containsTag("Ground", col.gameObject.tag))
            stop();
    }

    */