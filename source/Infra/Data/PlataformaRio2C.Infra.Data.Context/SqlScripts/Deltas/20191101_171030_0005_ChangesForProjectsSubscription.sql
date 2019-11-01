--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

sp_rename 'Projects.EasyEpisodePlayingTime', 'EachEpisodePlayingTime', 'COLUMN';