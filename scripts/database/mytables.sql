--�����û���
create table users (
	user_id             number
		generated by default as identity start with 1
	primary key,
	username            varchar2(50) not null unique,
	password            varchar2(50) not null,
	email               varchar2(100) not null unique,
	registration_date   date default sysdate,
	user_status         varchar2(20) default 'active',
	gender              varchar2(20),
	bio                 varchar2(20),
	avatar              clob,
	user_role           varchar2(20) default 'user',
	language_preference varchar2(20) default 'chinese'
);

--����ֲ���
create table plants (
	plant_id           number
		generated by default as identity start with 1
	primary key,
	common_name        varchar2(100),
	scientific_name    varchar2(100),
	category           varchar2(100),
	portrayal          clob,
	growth_environment varchar2(255),
	care_conditions    clob
);

--�������۱�
create table comments (
	comment_id      number
		generated by default as identity start with 1
	primary key,
	user_id         number
		references users ( user_id ),
	plant_id        number
		references plants ( plant_id ),
	comment_content clob,
	created_date    date default sysdate
);

--����˽�ű�
create table messages (
	message_id      number
		generated by default as identity start with 1
	primary key,
	sender_id       number
		references users ( user_id ),
	receiver_id     number
		references users ( user_id ),
	message_content clob,
	sent_date       date default sysdate
);

--�����ղر�
create table favorites (
	favorite_id number
		generated by default as identity start with 1
	primary key,
	user_id     number
		references users ( user_id ),
	plant_id    number
		references plants ( plant_id )
);
--�����ղر�
CREATE TABLE FavoritesArticles (
    favorite_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    user_id NUMBER REFERENCES Users(user_id),
    article_id NUMBER REFERENCES Articles(article_id)
);

--�������º͹����
CREATE TABLE Articles (
    article_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    article_title VARCHAR2(200),
    article_content CLOB,
    publisher_id NUMBER REFERENCES Users(user_id),
    published_date DATE DEFAULT SYSDATE,
    article_type VARCHAR2(50) DEFAULT 'article'  -- 'article' or 'announcement'
);

--�������۱�
CREATE TABLE Comments (
    comment_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    user_id NUMBER REFERENCES Users(user_id),
    article_id NUMBER REFERENCES Articles(article_id),
    comment_content CLOB,
    created_date DATE DEFAULT SYSDATE
);

--������˱�
CREATE TABLE Reviews (
    review_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    plant_id NUMBER REFERENCES Plants(plant_id),
    submitter_id NUMBER REFERENCES Users(user_id),
    modified_content CLOB,
    review_status VARCHAR2(20) DEFAULT 'pending',
    submitted_date DATE DEFAULT SYSDATE,
    review_date DATE
);

--�����û����ֱ�
CREATE TABLE UserPoints (
    points_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    user_id NUMBER REFERENCES Users(user_id),
    points NUMBER DEFAULT 0,
    last_updated DATE DEFAULT SYSDATE
);

--�����û����
CREATE TABLE UserGroups (
    group_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    group_name VARCHAR2(100),
    creator_id NUMBER REFERENCES Users(user_id),
    created_date DATE DEFAULT SYSDATE
);

--����������־��
CREATE TABLE CareLogs (
    log_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    user_id NUMBER REFERENCES Users(user_id),
    plant_id NUMBER REFERENCES Plants(plant_id),
    care_activity CLOB,
    log_date DATE DEFAULT SYSDATE
);

--�����û�������
CREATE TABLE Feedbacks (
    feedback_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    user_id NUMBER REFERENCES Users(user_id),
    feedback_content CLOB,
    submitted_date DATE DEFAULT SYSDATE
);

--����ֲ��ͼƬ��
CREATE TABLE PlantImages (
    image_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    plant_id NUMBER REFERENCES Plants(plant_id),
    user_id NUMBER REFERENCES Users(user_id),
    image_url VARCHAR2(255),
    recognition_result CLOB,
    diagnosis_result CLOB,
    upload_date DATE DEFAULT SYSDATE
);

--�������������Ա�
CREATE TABLE PlantRegionSuitability (
    suitability_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    plant_id NUMBER REFERENCES Plants(plant_id),
    region_name VARCHAR2(100),
    suitability VARCHAR2(255)
);

--�����û����־��
CREATE TABLE UserActivityLogs (
    log_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    user_id NUMBER REFERENCES Users(user_id),
    activity_type VARCHAR2(100),
    activity_description CLOB,
    activity_date DATE DEFAULT SYSDATE
);

--�����������ѷ����
CREATE TABLE CareReminders (
    reminder_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    plant_id NUMBER REFERENCES Plants(plant_id),
    user_id NUMBER REFERENCES Users(user_id),
    reminder_text VARCHAR2(255),
    reminder_date DATE,
    is_active CHAR(1) DEFAULT 'Y'
);

--�����в�ҩ��
CREATE TABLE MedicinalHerbs (
    herb_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    plant_id NUMBER REFERENCES Plants(plant_id),
    medicinal_properties CLOB,
    usage_instructions CLOB
);

--删除对应的文章时，删除相关评论
CREATE OR REPLACE TRIGGER Delete_Comments_Trigger
AFTER DELETE ON Articles
FOR EACH ROW
BEGIN
    DELETE FROM Comments WHERE article_id = :OLD.article_id;
END;
/

CREATE OR REPLACE TRIGGER Add_Article_P_Trigger
AFTER INSERT ON Articles
FOR EACH ROW
BEGIN
    -- 插入新的积分记录，给发布文章的用户增加 5 分
    INSERT INTO UserPoints (user_id, points, last_updated)
    VALUES (:NEW.publisher_id, 5, SYSDATE);
END;
/

CREATE OR REPLACE TRIGGER Add_Comment_P_Trigger
AFTER INSERT ON Comments
FOR EACH ROW
BEGIN
    -- 插入新的积分记录，给发表评论的用户增加 2 分
    INSERT INTO UserPoints (user_id, points, last_updated)
    VALUES (:NEW.user_id, 2, SYSDATE);
END;
/


