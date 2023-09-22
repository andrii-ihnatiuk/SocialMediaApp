CREATE TABLE public.users (
    id SERIAL PRIMARY KEY,
    username VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL
);

CREATE TABLE public.posts (
    id SERIAL PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    body TEXT NOT NULL,
    authorid INTEGER NOT NULL,
    CONSTRAINT posts_authorid_fkey FOREIGN KEY (authorid)
        REFERENCES public.users (id)
);

CREATE TABLE public.likes (
    userid INTEGER NOT NULL,
    postid INTEGER NOT NULL,
	CONSTRAINT likes_userid_postid_pkey PRIMARY KEY (userid, postid),
    CONSTRAINT likes_postid_fkey FOREIGN KEY (postid)
        REFERENCES public.posts (id),
    CONSTRAINT likes_userid_fkey FOREIGN KEY (userid)
        REFERENCES public.users (id)
);

CREATE TABLE public.userrelationships (
    followerid INTEGER NOT NULL,
    followingid INTEGER NOT NULL,
    CONSTRAINT primary_key PRIMARY KEY (followerid, followingid),
    CONSTRAINT userrelationships_followerid_fkey FOREIGN KEY (followerid)
        REFERENCES public.users (id),
    CONSTRAINT userrelationships_followingid_fkey FOREIGN KEY (followingid)
        REFERENCES public.users (id)
);
