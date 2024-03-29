package ru.thedreamingsaviour.game.resourceloader;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.audio.Music;

import java.io.File;
import java.util.*;

public class MusicLoader {
    private static Music deathMusic;
    private static Music menuMusic;
    private static Music factoryMusic;
    private static Music introductionMusic;
    private static Music rexDuodecimAngelusMusic;
    public static final Map<String, List<Music>> HUB_MUSIC = new HashMap<>();
    public static void load() {
        deathMusic = Gdx.audio.newMusic(Gdx.files.internal("music/eternity_served_cold.mp3"));
        menuMusic = Gdx.audio.newMusic(Gdx.files.internal("music/biophosphoradelecrystalluminescence.mp3"));
        factoryMusic = Gdx.audio.newMusic(Gdx.files.internal("music/another_medium.mp3"));
        introductionMusic = Gdx.audio.newMusic(Gdx.files.internal("music/overture.mp3"));
        rexDuodecimAngelusMusic = Gdx.audio.newMusic(Gdx.files.internal("music/rex_duodecim_angelus.mp3"));

        fillMusicMap(HUB_MUSIC, "music/hub_music/", "ACT_1");
        fillMusicMap(HUB_MUSIC, "music/hub_music/", "ACT_2");
    }

    private static void fillMusicMap(Map<String, List<Music>> map, String path, String key) {
        List<Music> musicList = new ArrayList<>();
        readAnimationTextures(musicList, path + key.toLowerCase() + "/");
        map.put(key, musicList);
    }

    private static void readAnimationTextures(List<Music> musicList, String path) {
        File directory = new File(Gdx.files.getLocalStoragePath() + "core/assets/" + path);
        int count = Objects.requireNonNull(directory.listFiles()).length;
        for (int i = 1; i <= count; i++) {
            musicList.add(Gdx.audio.newMusic(Gdx.files.internal(path + i + ".mp3")));
        }
    }

    public static Music getDeathMusic() {
        return deathMusic;
    }

    public static Music getMenuMusic() {
        return menuMusic;
    }

    public static Music getFactoryMusic() {
        return factoryMusic;
    }

    public static Music getIntroductionMusic() {
        return introductionMusic;
    }

    public static Music getRexDuodecimAngelusMusic() {
        return rexDuodecimAngelusMusic;
    }
}
